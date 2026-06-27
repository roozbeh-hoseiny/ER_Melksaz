using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.BuildingBlocks.Application.Persistence;

public interface IWriteUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<PrimitiveResult<int>> SaveChangesWithResultAsync(CancellationToken cancellationToken);
}
