using ER.Melksaz.Modules.IdentityModule.Application.Persistence.Models;

namespace ER.Melksaz.Modules.IdentityModule.Application.Persistence;

public interface IIdentityReadRepository
{
    Task<UserReadQuery[]> GetUsers(CancellationToken cancellationToken);
}
