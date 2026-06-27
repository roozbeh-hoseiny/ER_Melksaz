namespace ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

public interface IDbErrorDetector
{
    string ProviderName { get; }
    bool IsUniqueConstraintError(Exception exception);
}
