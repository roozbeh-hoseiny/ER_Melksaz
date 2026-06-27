using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Extensions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " FirstOrErrorAsync "
    public async Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.GenerateFilter(predicate, sort, projection)
                .Map(query => query.RunQueryWithError(q =>
                    q.FirstOrDefaultAsync(cancellationToken),
                    nullErrorCreator))
                .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.FirstOrErrorAsync(
            predicate,
            sort,
            projection,
            () => nullError,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<TEntity>> FirstOrErrorAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecFirstOrDefaultAsync(spec, cancellationToken);
        if (result is null) return PrimitiveResult.Failure<TEntity>(nullErrorCreator.Invoke());
        return result;
    }
    public async Task<PrimitiveResult<TEntity>> FirstOrErrorAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.FirstOrErrorAsync<TEntity>(
            spec,
            () => nullError,
            cancellationToken)
            .ConfigureAwait(false);
    }
    public async Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecFirstOrDefaultAsync(spec, cancellationToken);
        if (result is null) return PrimitiveResult.Failure<TResult>(nullErrorCreator.Invoke());
        return result;
    }
    public async Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.FirstOrErrorAsync<TEntity, TResult>(
            spec,
            () => nullError,
            cancellationToken)
            .ConfigureAwait(false);
    }
    #endregion
}