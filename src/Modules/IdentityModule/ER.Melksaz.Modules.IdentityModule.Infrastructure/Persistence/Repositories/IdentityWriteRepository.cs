using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Core.WriteRepository;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Repositories;

internal sealed class IdentityWriteRepository :
    EFGenericPrimitiveWriteRepository<IdentityWriteDbContext>,
    IIdentityWriteRepository
{
    public IdentityWriteRepository(IdentityWriteDbContext dbContext) : base(dbContext)
    {
    }

    public PrimitiveResult AddUser(User src) => this.Add(src);
}