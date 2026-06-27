using ER.Melksaz.Modules.IdentityModule.Infrastructure;
using ER.Melksaz.ServiceInstaller;
using System.Reflection;

namespace ER.Melksaz.Modules.IdentityModule.Api.DI;

public sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [InfrastructureAssemblyReference.Assembly];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) => services;
}
