using ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Core.WriteRepository;

public abstract partial class EFGenericPrimitiveWriteRepository<TDbContext> :
    EFGenericPrimitiveReadRepository<TDbContext>,
    IEFGenericPrimitiveWriteRepository where TDbContext : DbContext
{
    #region " Constructors "
    protected EFGenericPrimitiveWriteRepository(TDbContext dbContext) : base(dbContext)
    {
    }
    #endregion

    public PrimitiveResult Add<TEntity>(TEntity entity)
    {
        if (entity is null) return PrimitiveResult.Failure(EFRepositoryErrors.Can_Not_Add_Null_Entity_Error.Value);
        _ = this._dbContext.Add(entity);
        return PrimitiveResult.Success();
    }
    public async Task<PrimitiveResult> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null) return PrimitiveResult.Failure(EFRepositoryErrors.Can_Not_Add_Null_Entity_Error.Value);
        _ = this._dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        return PrimitiveResult.Success();
    }
    public PrimitiveResult Update<TEntity>(TEntity entity)
    {
        if (entity is null) return PrimitiveResult.Failure(EFRepositoryErrors.Can_Not_Update_Null_Entity_Error.Value);
        _ = this._dbContext.Update(entity);
        return PrimitiveResult.Success();
    }
}
