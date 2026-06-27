using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;

public abstract partial class EFGenericPrimitiveReadRepository<TDbContext>
{
    #region " FirstOrDefaultByIdAsync "
    /// <summary>
    /// Asynchronously retrieves the first entity of type <typeparamref name="TEntity"/> that matches the given primary key value.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type to query from the database context.
    /// </typeparam>
    /// <typeparam name="TId">
    /// The type of the primary key value.
    /// </typeparam>
    /// <param name="id">
    /// The primary key value used to locate the entity.
    /// </param>
    /// <param name="cancellationToken">
    /// A token used to cancel the asynchronous database operation.
    /// </param>
    /// <returns>
    /// A <see cref="PrimitiveResult{TEntity}"/> containing:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// The found entity if the operation succeeds.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// A failure result if:
    /// <para>- The provided <paramref name="id"/> is null.</para>
    /// <para>- The entity has a composite primary key.</para>
    /// <para>- No entity with the given id exists.</para>
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method only supports entities with a single primary key.
    /// If the entity has a composite key, the method returns a failure result.
    /// </remarks>

    public async Task<PrimitiveResult<TEntity>> FirstOrDefaultByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken)
    where TEntity : class
    {
        if (id is null)
            return PrimitiveResult.Failure<TEntity>(EFRepositoryErrors.Given_Id_Is_Null_Error.Value);

        var dbResult = await this.GetPrimaryKeys<TEntity>()
            .Ensure(primaryKeys => primaryKeys.Length == 1, EFRepositoryErrors.Generate_Entity_HasComplexPrimaryKey_Error<TEntity>())
            .Map(primaryKeys => this.GenerateEntityPrimaryKeyEqualityExpression<TEntity>(primaryKeys, id!))
            .Map(predicate => this._dbContext.Set<TEntity>()!
                .RunQueryWithError(
                    q => q.FirstOrDefaultAsync(predicate, cancellationToken),
                    EFRepositoryErrors.Entity_With_Id_Not_Found_Error.Value))
            .ConfigureAwait(false);
        return dbResult!;
    }
    #endregion
}