using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Reflection;

namespace ER.Melksaz.ServiceInstaller;

public static class ServiceInstallerHelper
{
    private static readonly Hashtable _installedAssemblies = [];

    public static IServiceCollection InstallServicesRecursively(
        IServiceCollection services,
        IConfiguration config,
        IHostEnvironment environment,
        params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(a => IsAssignableToType<IServiceInstaller>(a))
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in serviceInstallers)
        {
            _ = Install(services, config, environment, installer);
        }


        return services;
    }

    public static IServiceCollection Install(
        IServiceCollection services,
        IConfiguration config,
        IHostEnvironment environment,
        IServiceInstaller installer)
    {
        if (installer.DependantAssemblies?.Any() ?? false)
        {
            foreach (var dependantAssembly in installer.DependantAssemblies)
            {
                if (_installedAssemblies.Contains(dependantAssembly.FullName!)) continue;
                _ = InstallServicesRecursively(services, config, environment, [dependantAssembly]);
                _installedAssemblies.Add(dependantAssembly.FullName!, dependantAssembly.FullName!);
            }
        }

        return installer.InstallService(services, config, environment);
    }


    public static IServiceCollection Install<TInstaller>(IServiceCollection services, IConfiguration config, IHostEnvironment environment)
        where TInstaller : IServiceInstaller, new() => Install(services, config, environment, new TInstaller());

    public static IServiceCollection InstallServices(
        IServiceCollection services,
        IConfiguration config,
        IHostEnvironment environment,
        params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(a => IsAssignableToType<IServiceInstaller>(a))
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in serviceInstallers)
        {
            _ = installer.InstallService(services, config, environment);
        }

        return services;
    }

    private static bool IsAssignableToType<T>(TypeInfo typeInfo)
        => typeof(T).IsAssignableFrom(typeInfo)
            && !typeInfo.IsInterface
            && !typeInfo.IsAbstract;
}
