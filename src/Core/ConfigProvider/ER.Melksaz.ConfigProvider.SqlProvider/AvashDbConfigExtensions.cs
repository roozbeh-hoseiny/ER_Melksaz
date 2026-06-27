using Dapper;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueConverters;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.ConfigProvider.SqlProvider;

public static class AvashDbConfigExtensions
{
    public static IConfigurationBuilder AddDbConfig(this ConfigurationManager manager,
        string connectionString,
        string environment,
        SettingApplicationName applicationName,
        SettingVersion version,
        TimeSpan? pollingInterval,
        bool reloadOnChange)
    {
        IConfigurationBuilder configBuilder = manager;
        return AddDbConfigSource(
            configBuilder,
            connectionString,
            environment,
            applicationName,
            version,
            pollingInterval,
            reloadOnChange);
    }

    public static IConfigurationBuilder AddDbConfig(this IConfigurationBuilder configBuilder,
       string connectionString,
       string environment,
       SettingApplicationName applicationName,
       SettingVersion version,
       TimeSpan? pollingInterval,
       bool reloadOnChange)
    {
        return AddDbConfigSource(
            configBuilder,
            connectionString,
            environment,
            applicationName,
            version,
            pollingInterval,
            reloadOnChange);
    }

    private static void AddTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new SettingApplicationNameConverter());
        SqlMapper.AddTypeHandler(new SettingVersionConverter());
        SqlMapper.AddTypeHandler(new SettingKeyConverter());

    }

    private static IConfigurationBuilder AddDbConfigSource(IConfigurationBuilder configBuilder,
        string connectionString,
        string environment,
        SettingApplicationName applicationName,
        SettingVersion version,
        TimeSpan? pollingInterval,
        bool reloadOnChange)
    {
        AddTypeHandlers();
        return configBuilder.Add(new AvashDbConfigSource(connectionString,
           reloadOnChange,
           environment,
           applicationName,
           version,
           pollingInterval ?? TimeSpan.FromMinutes(1)));
    }

}