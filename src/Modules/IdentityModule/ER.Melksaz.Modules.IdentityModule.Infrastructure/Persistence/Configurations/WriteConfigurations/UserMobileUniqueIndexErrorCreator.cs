using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.WriteConfigurations;

internal sealed class UserMobileUniqueIndexErrorCreator : IDbUniqueErrorCreator
{
    public PrimitiveError? Create(string message)
    {
        if (message.Contains(IdentitySchemaInfo.Users_Mobile_UniqueIndexName))
            return ER.Melksaz.BuildingBlocks.Domain.DomainErrorCodes.CreateUniqueConstraintError("شماره موبایل تکراری است.");
        return null;
    }
}
