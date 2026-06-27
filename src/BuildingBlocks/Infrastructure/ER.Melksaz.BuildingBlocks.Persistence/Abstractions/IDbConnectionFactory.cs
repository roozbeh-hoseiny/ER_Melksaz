using System.Data;

namespace ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
