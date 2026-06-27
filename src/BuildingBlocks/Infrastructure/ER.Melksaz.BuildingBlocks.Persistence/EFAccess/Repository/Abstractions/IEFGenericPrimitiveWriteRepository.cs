namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Abstractions;

public interface IEFGenericPrimitiveWriteRepository
{
    PrimitiveResult Add<TEntity>(TEntity entity);
    Task<PrimitiveResult> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken);
    PrimitiveResult Update<TEntity>(TEntity entity);
}
