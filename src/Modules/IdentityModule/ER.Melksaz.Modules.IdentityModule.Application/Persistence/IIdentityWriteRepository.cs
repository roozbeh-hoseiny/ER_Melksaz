using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Application.Persistence;

public interface IIdentityWriteRepository
{
    PrimitiveResult AddUser(User src);
}
