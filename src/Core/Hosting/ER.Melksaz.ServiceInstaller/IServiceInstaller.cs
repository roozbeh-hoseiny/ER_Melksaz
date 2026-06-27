using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ER.Melksaz.ServiceInstaller;

public interface IServiceInstaller
{
    Assembly[]? DependantAssemblies { get; }

    IServiceCollection InstallService(IServiceCollection services, IConfiguration config);
}
