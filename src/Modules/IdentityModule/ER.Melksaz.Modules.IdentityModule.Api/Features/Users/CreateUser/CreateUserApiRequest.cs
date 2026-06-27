using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.Users.CreateUser;

public sealed record CreateUserApiRequest(
    string FirstName,
    LastName LastName,
    NationalCode NationalCode,
    Mobile Mobile,
    Email? Email,
    Username Username,
    string password);

