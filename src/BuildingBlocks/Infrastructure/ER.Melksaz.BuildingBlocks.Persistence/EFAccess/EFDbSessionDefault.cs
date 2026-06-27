using ER.Melksaz.BuildingBlocks.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess;

public sealed class EFDbSessionDefault<TContext> : DbSessionBase
    where TContext : DbContext
{
    public EFDbSessionDefault(TContext context) : base(context.Database.GetDbConnection())
    {
    }
}