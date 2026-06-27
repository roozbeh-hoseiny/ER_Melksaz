using Dapper;
using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;

namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public interface IDapperSqlSpecification
{
    SqlResult ToSql();
}

public interface IDapperSpecification : IBaseSpecification
{
    string GetCriteria();
    DynamicParameters GetParameters();
}
public interface IDapperProjectionSpecification : IDapperSpecification
{
    string GetQuery();
}
