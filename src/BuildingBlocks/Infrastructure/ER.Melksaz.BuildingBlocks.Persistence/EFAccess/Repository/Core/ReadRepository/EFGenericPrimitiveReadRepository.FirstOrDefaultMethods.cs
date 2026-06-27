using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Extensions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " FirstOrDefaultAsync "
    /// <summary>
    /// Retrieves the first entity matching the predicate, applies sorting and projection, or returns a default value if no match is found.
    /// </summary>
    public async Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, object>>? sort,
       Expression<Func<TEntity, TResult>>? projection,
       Func<TResult> defaultValueCreator,
       CancellationToken cancellationToken) where TEntity : class
    {
        return await this.GenerateFilter(predicate, sort, projection)
            .Map(query => query.RunQuery(q => q.FirstOrDefaultAsync(cancellationToken), defaultValueCreator))
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the first entity matching the predicate, applies sorting and projection, or returns the provided default value if no match is found.
    /// </summary>
    public async Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        TResult defaultValue,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await this.FirstOrDefaultAsync(
            predicate,
            sort,
            projection,
            () => defaultValue,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the first entity matching the specification or returns a failure result using the error creator.
    /// </summary>
    public async Task<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecFirstOrDefaultAsync(spec, cancellationToken);
        if (result is null) return PrimitiveResult.Failure<TEntity>(nullErrorCreator.Invoke());
        return result;
    }

    /// <summary>
    /// Retrieves the first entity matching the specification or returns a failure result with the provided error.
    /// </summary>
    public async Task<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.FirstOrDefaultAsync<TEntity>(
            spec,
            () => nullError,
            cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the first projected result matching the specification or returns a failure result using the error creator.
    /// </summary>
    public async Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecFirstOrDefaultAsync(spec, cancellationToken);
        if (result is null) return PrimitiveResult.Failure<TResult>(nullErrorCreator.Invoke());
        return result;
    }

    /// <summary>
    /// Retrieves the first projected result matching the specification or returns a failure result with the provided error.
    /// </summary>
    public async Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
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