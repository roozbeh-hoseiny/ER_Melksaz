using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " PaginateByPrimaryKey "
    public async Task<PrimitiveResult<PaginatedResult<TResult, TKey>>> PaginateByPrimaryKey<TEntity, TKey, TResult>(
        TKey? lastSeenKey,
        Expression<Func<TEntity, bool>>? predicate,
        int pageSize,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return await this.DbContext.PaginateByPrimaryKey(
                this.DbContext.Set<TEntity>(),
                lastSeenKey,
                predicate,
                pageSize,
                projection,
                cancellationToken)
                .ConfigureAwait(false);
    }
    #endregion
}