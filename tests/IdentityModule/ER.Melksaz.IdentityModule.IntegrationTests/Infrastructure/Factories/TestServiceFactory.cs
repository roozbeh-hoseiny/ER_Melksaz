using ER.Melksaz.Modules.IdentityModule.Infrastructure;
using ER.Melksaz.ServiceInstaller;
using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Factories;

public static class TestServiceFactory
{
    public static IServiceProvider Create(SqlServerFixture fixture)
    {
        var services = new ServiceCollection();

        // 🔥 mimic real app configuration
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Databases:ER_Melksaz:ConnectionString"] = fixture.ConnectionString,
                ["Databases:ER_Melksaz:UserId"] = fixture.UserId,
                ["Databases:ER_Melksaz:Password"] = fixture.Password,

                ["Hasher:HashKey"] = new string('*', 30),

                ["Encryption:HashKey"] = new string('*', 32),
                ["Encryption:Keys:0:Id"] = "A",
                ["Encryption:Keys:0:KeyString"] = new string('*', 32),

            })
            .Build();

        ServiceInstallerHelper.InstallServicesRecursively(
            services,
            configuration,
            InfrastructureAssemblyReference.Assembly);

        return services.BuildServiceProvider();
    }
}