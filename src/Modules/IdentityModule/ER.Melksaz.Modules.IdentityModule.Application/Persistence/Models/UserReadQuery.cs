using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

namespace ER.Melksaz.Modules.IdentityModule.Application.Persistence.Models;

public sealed class UserReadQuery
{
    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public NationalCode NationalCode { get; private set; }
    public Mobile Mobile { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash Password { get; private set; }
    public bool IsActive { get; private set; }
}
