using System.Data.Common;

namespace ER.Melksaz.BuildingBlocks.Persistence.Core;

public sealed class ADODbSessionDefault : DbSessionBase
{
    public ADODbSessionDefault(DbConnection dbConnection) : base(dbConnection) { }
}
