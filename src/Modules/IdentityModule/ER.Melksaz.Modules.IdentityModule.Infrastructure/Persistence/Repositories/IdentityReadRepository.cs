using ER.Melksaz.BuildingBlocks.Infrastructure.Persistence.EFAccess.Repository.Core.ReadRepository;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence.Models;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Repositories;

internal sealed class IdentityReadRepository :
    EFGenericPrimitiveReadRepository<IdentityReadDbContext>,
    IIdentityReadRepository
{
    public IdentityReadRepository(IdentityReadDbContext dbContext) : base(dbContext)
    {
    }

    public Task<UserReadQuery[]> GetUsers(CancellationToken cancellationToken) =>
        this.DbContext.Users.ToArrayAsync(cancellationToken);
}