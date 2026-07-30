using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Core.WriteRepository;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository.Extensions;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.AccountAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using ER.Melksaz.PrimitiveResults;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Repositories;

internal sealed class IdentityWriteRepository :
    EFGenericPrimitiveWriteRepository<IdentityWriteDbContext>,
    IIdentityWriteRepository
{
    public IdentityWriteRepository(IdentityWriteDbContext dbContext) : base(dbContext)
    {
    }

    public PrimitiveResult AddUser(User src) => this.Add(src);
    public PrimitiveResult AddAccount(AccountLedger src) => this.Add(src);
    public async Task<PrimitiveResult<List<AccountLedger>>> GetAllAccounts(CancellationToken cancellationToken)
    {
        return await this.DbContext
            .AccountLedgers
            .RunQuery(q => q.ToListAsync(), () => Array.Empty<AccountLedger>().ToList());
    }
    public async Task<PrimitiveResult<AccountLedger>> GetOne(int id, CancellationToken cancellationToken)
    {
        return await this.DbContext
            .AccountLedgers
            .Where(x => x.Id == id)
            .RunQueryWithError(q => q.FirstOrDefaultAsync(), PrimitiveError.Create("", ""));
    }
}