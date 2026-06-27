using ER.Melksaz.ConfigProvider.SqlProvider;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ER.Melksaz.Hosting;

public static class AppConfigurationHelper
{
    private const string EnvSecretPrefix = "${ENV:";
    private const string EnvSecretSuffix = "}";

    public static void ConfigureAppConfiguration(
        IConfigurationBuilder config,
        IConfiguration existingConfiguration,
        string dbConfigSectionName,
        SettingVersion appVersion)
    {
        var currentEnv = ServiceInfoHelper.GetEnvValue(
                 existingConfiguration,
                 ["env", "", "DOTNET_ENVIRONMENT", "ASPNETCORE_ENVIRONMENT"],
                 "Development");

        // Base config
        config
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{currentEnv}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        // Resolve secrets before reading connection string
        ApplyEnvironmentSecretReferences(config);

        // Build temporary configuration once
        var configuration = config.Build();

        var serviceName = ServiceInfoHelper.GetEntryProjectName(configuration);
        Console.Title = $"{serviceName}({currentEnv})-{DateTimeOffset.Now:yyyy/MM/dd HH:mm:ss.ffff}";
        Console.WriteLine($"Service = {serviceName}, Env = {currentEnv}");

        var connectionString =
            configuration.GetRequiredSection(dbConfigSectionName).Get<string>()
            ?? throw new InvalidOperationException($"Configuration section '{dbConfigSectionName}' was not found.");

        // Add DB-backed config
        config.AddDbConfig(
            connectionString,
            currentEnv,
            new SettingApplicationName(serviceName),
            appVersion,
            TimeSpan.FromSeconds(5),
            reloadOnChange: true);

        // Optional: apply again if DB config itself contains secret refs
        ApplyEnvironmentSecretReferences(config);
    }

    public static void ConfigureHostedAppConfiguration(
         HostBuilderContext context,
         IConfigurationBuilder config,
         string dbConfigSectionName,
         SettingVersion appVersion)
    {
        ConfigureAppConfiguration(
            config,
            context.Configuration,
            dbConfigSectionName,
            appVersion);
    }

    public static void ConfigureWebAppConfiguration(
        WebApplicationBuilder builder,
        string dbConfigSectionName,
        SettingVersion appVersion)
    {
        ConfigureAppConfiguration(
            builder.Configuration,
            builder.Configuration,
            dbConfigSectionName,
            appVersion);
    }

    private static void ApplyEnvironmentSecretReferences(IConfigurationBuilder config)
    {
        var configuration = config.Build();
        Dictionary<string, string?> overrides = new(StringComparer.OrdinalIgnoreCase);

        foreach (var item in configuration.AsEnumerable())
        {
            if (string.IsNullOrWhiteSpace(item.Key) || string.IsNullOrWhiteSpace(item.Value)) continue;
            if (!TryGetEnvironmentVariableName(item.Value!, out var envVarName)) continue;

            var secretValue = Environment.GetEnvironmentVariable(envVarName);

            if (string.IsNullOrWhiteSpace(secretValue))
            {
                Console.WriteLine($"[SecretRef] Missing environment variable '{envVarName}' for key '{item.Key}'.");
                continue;
            }

            overrides[item.Key] = secretValue;
        }

        if (overrides.Count > 0)
        {
            _ = config.AddInMemoryCollection(overrides);
        }
    }

    private static bool TryGetEnvironmentVariableName(string value, out string environmentVariableName)
    {
        environmentVariableName = string.Empty;

        if (!value.StartsWith(EnvSecretPrefix, StringComparison.OrdinalIgnoreCase)) return false;
        if (!value.EndsWith(EnvSecretSuffix, StringComparison.OrdinalIgnoreCase)) return false;

        var envVarName = value[EnvSecretPrefix.Length..^EnvSecretSuffix.Length].Trim();
        if (string.IsNullOrWhiteSpace(envVarName)) return false;

        environmentVariableName = envVarName;
        return true;
    }
}
