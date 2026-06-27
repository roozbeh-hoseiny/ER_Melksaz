using ER.Melksaz.Hosting.LogEnrichers;
using ER.Melksaz.ServiceInstaller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ER.Melksaz.Hosting;

public static class HostedServiceServiceInstallerHelper
{
    public static IServiceCollection InstallHostedServiceServices(
        IServiceCollection services,
        IConfiguration config,
        params Assembly[] assemblies)
    {
        _ = services.AddTransient<ServiceInfoEnricher>();

        return ServiceInstallerHelper.InstallServicesRecursively(services,
                config,
                assemblies);
    }
}