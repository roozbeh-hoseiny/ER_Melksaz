using ER.Melksaz.BuildingBlocks.Application.Persistence;

namespace ER.Melksaz.Modules.IdentityModule.Application.Persistence;

public interface IIdentityUnitOfWork : IWriteUnitOfWork
{
    IIdentityWriteRepository WriteRepo { get; }
}
