using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.WriteConfigurations;

internal sealed class UserEmailUniqueIndexErrorCreator : IDbUniqueErrorCreator
{
    public PrimitiveError? Create(string message)
    {
        if (message.Contains(IdentitySchemaInfo.Users_Email_UniqueIndexName))
            return ER.Melksaz.BuildingBlocks.Domain.DomainErrorCodes.CreateUniqueConstraintError("ایمیل تکراری است.");
        return null;
    }
}
