using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.BuildingBlocks.Persistence.Core;

public enum DbConnectionMode
{
    Default,
    ReadOnly,
    ReadWrite
}

public static class DbConnectionSelector
{
    public static IConfigurationSection GetConnectionConfig(
        IConfiguration config,
        string sectionName,
        DbConnectionMode mode)
    {
        var defaultConfig = config.GetSection(sectionName);
        var result = mode switch
        {
            DbConnectionMode.ReadOnly => config.GetSection($"{sectionName}_read"),
            DbConnectionMode.ReadWrite => config.GetSection($"{sectionName}_write"),
            _ => defaultConfig
        };

        if (result is null && defaultConfig is null)
            throw new InvalidOperationException($"Cannot find db config for '{sectionName}' with mode '{mode}'.");

        if (result?.Get<object>() is not null) return result;

        return defaultConfig;
    }
}