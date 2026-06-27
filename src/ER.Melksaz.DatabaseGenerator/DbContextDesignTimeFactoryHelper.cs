using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;
using ER.Melksaz.Modules.IdentityModule.Infrastructure;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AvashSampleApp.DatabaseGenerator;

internal static class DbContextDesignTimeFactoryHelper
{
    public static TDbContext CreateDbContext<TDbContext>(
        string[] args,
        string dbSectionName,
        string schemaName,
        Assembly assembly,
        Func<DbContextOptions<TDbContext>, EntityTypeConfigurationDependency[], TDbContext> dbContextFactory) where TDbContext : DbContext
    {

        var provider = Environment.GetEnvironmentVariable("EF_PROVIDER");
        if (string.IsNullOrWhiteSpace(provider)) provider = "sql";

        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                         ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                         ?? "Development";


        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.{provider}.json", optional: true, reloadOnChange: true)
            .Build();

        var cnnstring = PersistenceHelpers.GetConnectionstring(configuration,
            dbSectionName,
            DbConnectionMode.ReadWrite);

        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
        optionsBuilder.UseSqlServer(cnnstring);

        var allEntityTypeConfigurations = EFHelpers.FindEntityTypeConfigurationDependenciesOfSchema(schemaName, assembly)?
            .ToArray() ?? [];
        return dbContextFactory.Invoke(optionsBuilder.Options, allEntityTypeConfigurations);
    }
}
internal class IdentityWriteDbContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityWriteDbContext>
{
    public IdentityWriteDbContext CreateDbContext(string[] args)
    {
        return DbContextDesignTimeFactoryHelper.CreateDbContext<IdentityWriteDbContext>(
            args,
            "Database:ER_Melksaz",
            IdentityWriteDbContext.SchemaName,
            InfrastructureAssemblyReference.Assembly,
            (opts, configs) => new IdentityWriteDbContext(opts, configs));
    }
}
