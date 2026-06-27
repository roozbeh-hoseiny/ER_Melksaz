using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence;

public sealed class SqlServerErrorDetector : IDbErrorDetector
{
    public string ProviderName => "Microsoft.EntityFrameworkCore.SqlServer";

    public bool IsUniqueConstraintError(Exception exception)
    {
        return exception is DbUpdateException dbEx
            && dbEx.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
            && (sqlEx.Number == 2627 || sqlEx.Number == 2601);
    }
}