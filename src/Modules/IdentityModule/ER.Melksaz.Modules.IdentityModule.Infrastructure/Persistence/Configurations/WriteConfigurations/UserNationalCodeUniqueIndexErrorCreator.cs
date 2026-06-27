using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Configurations.WriteConfigurations;

internal sealed class UserNationalCodeUniqueIndexErrorCreator : IDbUniqueErrorCreator
{
    public PrimitiveError? Create(string message)
    {
        if (message.Contains(IdentitySchemaInfo.Users_NationalCode_UniqueIndexName))
            return ER.Melksaz.BuildingBlocks.Domain.DomainErrorCodes.CreateUniqueConstraintError("کد ملی تکراری است.");
        return null;
    }
}