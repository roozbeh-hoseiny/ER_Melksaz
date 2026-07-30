using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.AccountAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Application.Persistence;

public interface IIdentityWriteRepository
{
    PrimitiveResult AddUser(User src);
    PrimitiveResult AddAccount(AccountLedger src);

    Task<PrimitiveResult<List<AccountLedger>>> GetAllAccounts(CancellationToken cancellationToken);
    Task<PrimitiveResult<AccountLedger>> GetOne(int id, CancellationToken cancellationToken);

}
