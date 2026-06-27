using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.UnitOfWorks;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;

internal sealed class IdentityUnitOfWork :
    EFWriteUnitOfWorkBase<IdentityWriteDbContext>,
    IIdentityUnitOfWork
{
    public IIdentityWriteRepository WriteRepo => this.GetService<IIdentityWriteRepository>();

    public IdentityUnitOfWork(
        IdentityWriteDbContext context,
        IBaseDbSession sessionManager,
        IServiceProvider serviceProvider,
        IDbErrorResolver dbErrorResolver,
        IEnumerable<IDbUniqueErrorCreator> dbUniqueErrorCreators) : base(context, sessionManager, serviceProvider, dbErrorResolver, dbUniqueErrorCreators)
    {
    }
}
