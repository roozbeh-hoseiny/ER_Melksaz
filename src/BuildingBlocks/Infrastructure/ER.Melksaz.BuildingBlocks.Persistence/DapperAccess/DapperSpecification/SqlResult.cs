using Dapper;
using System.Text;

namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public sealed class SqlResult
{
    public string Sql { get; set; } = string.Empty;
    public DynamicParameters Parameters { get; set; } = new();

    public override string ToString()
    {
        var result = new StringBuilder();

        _ = result.AppendLine($"Query: {this.Sql}");
        _ = result.AppendLine($"Parameters: ");
        foreach (string p in this.Parameters.ParameterNames)
        {
            _ = result.AppendLine($"{p} : {this.Parameters.Get<object>(p)}");
        }

        return result.ToString();
    }
}
