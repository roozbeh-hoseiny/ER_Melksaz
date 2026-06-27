using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Extensions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " ToListAsync "
    public async Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        int maxCount,
        CancellationToken cancellationToken)
    where TEntity : class
    {
        return await this.GenerateFilter(predicate, sort, projection, maxCount)
          .Map(query => query.RunQuery(q => q.ToListAsync(cancellationToken), () => Array.Empty<TResult>().ToList()))
          .ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.ToListAsync(
            predicate,
            sort,
            projection,
            -1,
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<PrimitiveResult<List<TEntity>>> ToListAsync<TEntity>(
        IEFSpecification<TEntity> spec,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecToListAsync(spec, cancellationToken);
        if (result is null) return Array.Empty<TEntity>().ToList();
        return result;
    }
    public async Task<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
        ISpecification<TEntity, TResult> spec,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var result = await this._dbContext.Set<TEntity>().SpecToListAsync(spec, cancellationToken);
        if (result is null) return Array.Empty<TResult>().ToList();
        return result;
    }
    #endregion
}