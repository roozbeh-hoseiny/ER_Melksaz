using ER.Melksaz.BuildingBlocks.Application.Persistence;
using ER.Melksaz.BuildingBlocks.Domain.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.UnitOfWorks;

public abstract class EFWriteUnitOfWorkBase<TDbContext> : IWriteUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;
    protected readonly IBaseDbSession _sessionManager;
    protected readonly IServiceProvider _serviceProvider;
    private readonly IDbErrorResolver _dbErrorResolver;
    private readonly IEnumerable<IDbUniqueErrorCreator> _dbUniqueErrorCreators;

    protected EFWriteUnitOfWorkBase(
        TDbContext context,
        IBaseDbSession sessionManager,
        IServiceProvider serviceProvider,
        IDbErrorResolver dbErrorResolver,
        IEnumerable<IDbUniqueErrorCreator> dbUniqueErrorCreators)
    {
        this._context = context;
        this._sessionManager = sessionManager;
        this._serviceProvider = serviceProvider;
        this._dbErrorResolver = dbErrorResolver;
        this._dbUniqueErrorCreators = dbUniqueErrorCreators;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var trackedEvents = this.GetTrackedEvents();

        if (!this._sessionManager.IsTransactionStarted())
        {
            await this._sessionManager.BeginTransactionAsync(false, cancellationToken).ConfigureAwait(false);
            _ = await this._context.Database.UseTransactionAsync(this._sessionManager.Transaction).ConfigureAwait(false);
        }

        try
        {
            var result = await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _ = await this.PersistOutboxMessages(trackedEvents).ConfigureAwait(false);

            if (this._sessionManager.IsTransactionStarted())
                await this._sessionManager.CommitAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }
        catch
        {
            if (this._sessionManager.IsTransactionStarted())
            {
                await this._sessionManager.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }
            throw;
        }
    }
    protected T GetService<T>() where T : notnull => this._serviceProvider.GetRequiredService<T>()!;

    public async Task<PrimitiveResult<int>> SaveChangesWithResultAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await this.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            if (this._dbErrorResolver.IsUniqueConstraintError(ex, this._context))
            {
                var uniqueError = this.GenerateUniqueError(ex);
                if (uniqueError is not null) return PrimitiveResult.Failure<int>(uniqueError);
            }
            throw;
        }
    }
    private IDomainEvent[] GetTrackedEvents() => this._context.ChangeTracker
        .Entries<IEventEntity>()
        .Select(x => x.Entity)
        .SelectMany(eventEntity =>
        {
            var domainEvents = eventEntity.Events.ToList();
            eventEntity.ClearDomainEvents();
            return domainEvents;
        })
        .ToArray();
    private async ValueTask<int> PersistOutboxMessages(IDomainEvent[] @events)
    {
        if (events.Length == 0) return 0;

        var outboxMessages = new List<OutboxMessage>(events.Length);

        var timeStamp = DateTimeOffset.Now;

        for (var i = 0; i < @events.Length; i++)
        {
            var @event = EventFactory.Create(@events[i]);
            outboxMessages.Add(OutboxMessage.Create(@event, timeStamp));
            timeStamp = timeStamp.AddSeconds(1);
        }

        this._context.Set<OutboxMessage>()
            .AddRange(outboxMessages);

        return await this._context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
    }
    private PrimitiveError? GenerateUniqueError(Exception ex)
    {
        if (this._dbUniqueErrorCreators is null || !this._dbUniqueErrorCreators.Any()) return null;

        var dbErrorMessage = ex.InnerException?.Message ?? string.Empty;

        foreach (var uniqueErrorCreator in this._dbUniqueErrorCreators)
        {
            var result = uniqueErrorCreator.Create(dbErrorMessage);
            if (result is not null) return result;
        }
        return null;
    }
}
