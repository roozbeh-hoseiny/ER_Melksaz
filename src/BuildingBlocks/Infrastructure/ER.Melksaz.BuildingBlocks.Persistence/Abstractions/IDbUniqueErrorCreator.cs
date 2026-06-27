namespace ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

public interface IDbUniqueErrorCreator
{
    PrimitiveError? Create(string message);
}