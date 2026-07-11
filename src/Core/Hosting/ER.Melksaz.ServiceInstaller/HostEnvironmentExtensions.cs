using Microsoft.Extensions.Hosting;

namespace ER.Melksaz.ServiceInstaller;

public static class HostEnvironmentExtensions
{


    public static bool Is(
        this IHostEnvironment environment,
        string environmentName)
    {
        ArgumentNullException.ThrowIfNull(environment);
        ArgumentException.ThrowIfNullOrWhiteSpace(environmentName);

        return string.Equals(
            environment.EnvironmentName,
            environmentName,
            StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsTest(this IHostEnvironment environment) => environment.Is(HostingConstants.TestEnvironment);
    public static bool IsDevelopment(this IHostEnvironment environment) => environment.Is(HostingConstants.DevelopmentEnvironment);
    public static bool IsDebug(this IHostEnvironment environment) => environment.Is(HostingConstants.DebugEnvironment);
    public static bool IsProduction(this IHostEnvironment environment) => environment.Is(HostingConstants.ProductionEnvironment);
    public static bool IsStaging(this IHostEnvironment environment) => environment.Is(HostingConstants.StagingEnvironment);
}