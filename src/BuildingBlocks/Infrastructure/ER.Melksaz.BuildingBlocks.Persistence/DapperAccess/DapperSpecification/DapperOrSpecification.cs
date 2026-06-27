using Dapper;

namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public sealed class DapperOrSpecification : IDapperSqlSpecification
{
    private readonly IDapperSqlSpecification _left;
    private readonly IDapperSqlSpecification _right;

    public DapperOrSpecification(IDapperSqlSpecification left, IDapperSqlSpecification right)
    {
        this._left = left;
        this._right = right;
    }

    public SqlResult ToSql()
    {
        SqlResult left = this._left.ToSql();
        SqlResult right = this._right.ToSql();

        var parameters = new DynamicParameters();

        parameters.AddDynamicParams(left.Parameters);
        parameters.AddDynamicParams(right.Parameters);

        return new SqlResult
        {
            Sql = $"({left.Sql} OR {right.Sql})",
            Parameters = parameters
        };
    }
}
