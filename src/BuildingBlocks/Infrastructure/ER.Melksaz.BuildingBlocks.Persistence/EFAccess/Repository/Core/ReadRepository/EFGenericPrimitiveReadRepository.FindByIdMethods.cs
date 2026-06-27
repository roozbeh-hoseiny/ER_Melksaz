using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " FindByIdAsync "
    /// <summary>
    /// Asynchronously finds an entity with the given primary key value(s) using the underlying DbContext's FindAsync mechanism.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to be retrieved.</typeparam>
    /// <typeparam name="TId">The type of the primary key.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A <see cref="PrimitiveResult{TEntity}"/> which is:
    /// <list type="bullet">
    /// <item><description>Successful if the entity is found and tracked.</description></item>
    /// <item><description>A failure if the <paramref name="id"/> is null.</description></item>
    /// <item><description>A failure if no entity matches the provided <paramref name="id"/>.</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// Unlike query-based methods, FindAsync first checks the local tracker before hitting the database.
    /// </remarks>

    public async Task<PrimitiveResult<TEntity>> FindByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken) where TEntity : class
    {
        if (id is null)
            return PrimitiveResult.Failure<TEntity>(EFRepositoryErrors.Given_Id_Is_Null_Error.Value);

        var dbResult = await this._dbContext.Set<TEntity>()
                .FindAsync(id, cancellationToken)
                .ConfigureAwait(false);

        if (dbResult is null)
            return PrimitiveResult.Failure<TEntity>(EFRepositoryErrors.Entity_With_Id_Not_Found_Error.Value);

        return dbResult;
    }
    #endregion
}