using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;
using ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Containers;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using ER.Melksaz.ServiceInstaller;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.IdentityModule.IntegrationTests.Infrastructure.Factories;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly SqlServerContainer _sqlServerContainer;

    public CustomWebApplicationFactory(SqlServerContainer sqlServerContainer)
    {
        this._sqlServerContainer = sqlServerContainer;
    }

    public async Task InitializeAsync()
    {
        using var scope = this.Services.CreateScope();

        await IdentityMigrator.MigrateAsync(scope.ServiceProvider);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        IConfiguration? configuration = null;
        builder.UseEnvironment(HostingConstants.TestEnvironment);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Clear(); // 👈 مهم‌ترین خط

            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Databases:ER_Melksaz:ConnectionString"] = this._sqlServerContainer.ConnectionString,
                ["Databases:ER_Melksaz:UserId"] = this._sqlServerContainer.UserId,
                ["Databases:ER_Melksaz:Password"] = this._sqlServerContainer.Password,

                ["Hasher:HashKey"] = new string('*', 30),

                ["Encryption:HashKey"] = new string('*', 32),
                ["Encryption:Keys:0:Id"] = "A",
                ["Encryption:Keys:0:KeyString"] = new string('*', 32),
            });

            configuration = config.Build();
        });

        builder.ConfigureServices(services =>
        {
            SqlPersistenceHelpers.RegisterDbContext<IdentityWriteDbContext>(
                services,
                configuration!,
                Modules.IdentityModule.Infrastructure.DI.ProjectServiceInstaller.DatabaseSectionName,
                BuildingBlocks.Persistence.Core.DbConnectionMode.ReadWrite,
                Modules.IdentityModule.Infrastructure.InfrastructureAssemblyReference.Assembly);

            SqlPersistenceHelpers.RegisterDbContext<IdentityReadDbContext>(
                services,
                configuration!,
                Modules.IdentityModule.Infrastructure.DI.ProjectServiceInstaller.DatabaseSectionName,
                BuildingBlocks.Persistence.Core.DbConnectionMode.ReadOnly,
                Modules.IdentityModule.Infrastructure.InfrastructureAssemblyReference.Assembly);
        });
    }
}