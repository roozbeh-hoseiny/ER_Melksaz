using Dapper;

namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public sealed class DapperSqlSpecification : IDapperSqlSpecification
{
    #region " Fields "
    private readonly string _sql;
    private readonly object? _parameters;
    #endregion

    public DapperSqlSpecification(string sql, object? parameters = null)
    {
        this._sql = sql;
        this._parameters = parameters;
    }

    public SqlResult ToSql()
    {
        var result = new SqlResult
        {
            Sql = this._sql
        };

        if (this._parameters is not null)
        {
            result.Parameters.AddDynamicParams(this._parameters);
        }

        return result;
    }
}
public abstract class DapperSpecification : IDapperSpecification
{
    private const string ALWAYS_TRUE_CRITERIA = "1 = 1";
    private static readonly DynamicParameters EmptyParameters = new DynamicParameters();

    private IDapperSqlSpecification? _spec = null;
    private string _criteria = string.Empty;

    protected DapperSpecification()
    {

    }

    protected DapperSpecification SetCriteria(IDapperSqlSpecification spec)
    {
        this._spec = spec;
        return this;
    }

    public string GetCriteria()
    {
        if (!string.IsNullOrWhiteSpace(this._criteria)) return this._criteria;

        string criteria = this._spec is null
             ? ALWAYS_TRUE_CRITERIA
             : this._spec.ToSql().Sql;

        if (string.IsNullOrWhiteSpace(criteria))
        {
            criteria = ALWAYS_TRUE_CRITERIA;
        }

        this._criteria = criteria;

        return this._criteria;
    }

    public DynamicParameters GetParameters()
    {
        return this._spec?.ToSql().Parameters ?? EmptyParameters;
    }
}
public abstract class DapperProjectionSpecification : DapperSpecification, IDapperProjectionSpecification
{
    private readonly string _projection;

    protected DapperProjectionSpecification(string projection)
    {
        this._projection = projection;
    }
    public string GetQuery()
    {
        if (string.IsNullOrWhiteSpace(this._projection))
            throw new Exception("Projection is empty");

        return $"{this._projection} WHERE {this.GetCriteria()};";
    }
}