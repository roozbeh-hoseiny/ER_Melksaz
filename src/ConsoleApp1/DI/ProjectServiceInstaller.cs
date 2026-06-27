using ER.Melksaz.Modules.IdentityModule.Infrastructure;
using ER.Melksaz.ServiceInstaller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConsoleApp1.DI;

internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [
        InfrastructureAssemblyReference.Assembly
        ];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config)
    {
        services.AddHostedService<ServiceWorker>();
        return services;
    }
}

internal static class ProjectServiceCollectionExtension
{
}
