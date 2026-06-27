using ER.Melksaz.Modules.IdentityModule.Infrastructure;
using ER.Melksaz.ServiceInstaller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Melksaz.IdentityModule.IntegrationTests;

public static class TestServiceFactory
{
    public static IServiceProvider Create(SqlServerFixture fixture)
    {
        var services = new ServiceCollection();

        // 🔥 mimic real app configuration
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Databases:ER_Melksaz:ConnectionString"] = fixture.ConnectionString
            })
            .Build();

        ServiceInstallerHelper.InstallServicesRecursively(
            services,
            configuration,
            InfrastructureAssemblyReference.Assembly);

        return services.BuildServiceProvider();
    }
}