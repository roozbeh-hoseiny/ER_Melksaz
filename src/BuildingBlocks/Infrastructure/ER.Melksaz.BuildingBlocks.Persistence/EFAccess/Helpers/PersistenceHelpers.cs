using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.Models;
using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;

public static class PersistenceHelpers
{
    public static string GetConnectionstring(
        IConfiguration config,
        string sectionName,
        DbConnectionMode mode)
    {
        var connectionStringSection = DbConnectionSelector.GetConnectionConfig(config, sectionName, DbConnectionMode.ReadWrite);
        if (connectionStringSection is null)
        {
            ArgumentNullException.ThrowIfNull(connectionStringSection);
        }

        var dbConnectionConfig = connectionStringSection.Get<DbConnectionConfig>();
        if (dbConnectionConfig is null)
        {
            ArgumentNullException.ThrowIfNull(dbConnectionConfig);
        }

        return dbConnectionConfig.ToString();
    }
}