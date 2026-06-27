using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.PrimitiveResults;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Abstractions;

public interface IEFGenericPrimitiveReadRepository
{
    Task<PrimitiveResult<TEntity>> FindByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity>> FirstOrDefaultByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken)
        where TEntity : class;

    #region " FirstOrErrorAsync "
    Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity>> FirstOrErrorAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity>> FirstOrErrorAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrErrorAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class;

    #endregion

    #region " FirstOrDefaultAsync "
    Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        Func<TResult> defaultValueCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        TResult defaultValue,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        Func<PrimitiveError> nullErrorCreator,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class;
    #endregion

    #region " ToListAsync "
    Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        int maxCount,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<List<TEntity>>> ToListAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
         ISpecification<TEntity, TResult> spec,
         CancellationToken cancellationToken)
         where TEntity : class;

    #endregion

    #region " ToArrayAsync "
    Task<PrimitiveResult<TResult[]>> ToArrayAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult[]>> ToArrayAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        int maxCount,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TEntity[]>> ToArrayAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken)
        where TEntity : class;

    Task<PrimitiveResult<TResult[]>> ToArrayAsync<TEntity, TResult>(
         ISpecification<TEntity, TResult> spec,
         CancellationToken cancellationToken)
         where TEntity : class;
    #endregion

    #region " PaginateByPrimaryKey "
    Task<PrimitiveResult<PaginatedResult<TResult, TKey>>> PaginateByPrimaryKey<TEntity, TKey, TResult>(
        TKey? lastSeenKey,
        Expression<Func<TEntity, bool>>? predicate,
        int pageSize,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken) where TEntity : class;
    #endregion

}
