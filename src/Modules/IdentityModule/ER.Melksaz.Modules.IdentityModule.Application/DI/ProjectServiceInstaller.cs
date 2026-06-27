using ER.Melksaz.ServiceInstaller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ER.Melksaz.Modules.IdentityModule.Application.DI;

internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}
internal static class ProjectServiceCollectionExtension
{

}