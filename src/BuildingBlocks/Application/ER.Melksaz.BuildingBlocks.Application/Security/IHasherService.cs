namespace ER.Melksaz.BuildingBlocks.Application.Security;

public interface IHasherService
{
    byte[] Hash(string val);
}