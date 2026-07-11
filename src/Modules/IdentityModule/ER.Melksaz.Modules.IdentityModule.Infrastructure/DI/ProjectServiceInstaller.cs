using ER.Melksaz.BuildingBlocks.Application.Security;
using ER.Melksaz.BuildingBlocks.Infrastructure.Security;
using ER.Melksaz.BuildingBlocks.Persistence;
using ER.Melksaz.BuildingBlocks.Persistence.Abstractions;
using ER.Melksaz.BuildingBlocks.Persistence.Core;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Configurations;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;
using ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Interceptors;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.Repositories;
using ER.Melksaz.Security.Encryption;
using ER.Melksaz.ServiceInstaller;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.DI;

internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public const string DatabaseSectionName = "Databases:ER_Melksaz";

    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(
        IServiceCollection services,
        IConfiguration config,
        IHostEnvironment environment)
    {
        _ = services.Configure<HasherServiceOptions>(options =>
        {
            config.GetSection("Hasher").Bind(options);
        });
        _ = services.Configure<EncryptionServiceOptions>(options =>
        {
            config.GetSection("Encryption").Bind(options);
        });

        services.TryAddSingleton<IEncryptionService, DefaultEncryptionService>();
        services.TryAddSingleton<IHasherService, HasherService>();

        _ = services.InstallPersistence(config, environment, DatabaseSectionName);

        services.TryAddScoped<IIdentityReadRepository, IdentityReadRepository>();

        services.Scan(scan =>
            scan
                .FromAssemblies(InfrastructureAssemblyReference.Assembly)
                .AddClasses(classes => classes.AssignableTo<IDbUniqueErrorCreator>(), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
internal static class ProjectServiceCollectionExtension
{
    internal static IServiceCollection InstallPersistence(
        this IServiceCollection services,
        IConfiguration config,
        IHostEnvironment environment,
        string databaseSectionName)
    {
        services.TryAddScoped<IBaseDbSession, EFDbSessionDefault<IdentityWriteDbContext>>();

        services.TryAddScoped<IIdentityWriteRepository, IdentityWriteRepository>();

        services.TryAddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        services.TryAddScoped<IIdentityReadRepository, IdentityReadRepository>();

        var allDefinedTypes = InfrastructureAssemblyReference.Assembly
                .DefinedTypes
                .Where(t => !t.IsAbstract
                            && !t.IsGenericTypeDefinition
                            && typeof(EntityTypeConfigurationDependency).IsAssignableFrom(t))
                .Distinct()
                .ToArray();

        foreach (var type in allDefinedTypes)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Describe(typeof(EntityTypeConfigurationDependency),
                type,
                ServiceLifetime.Singleton));
        }

        _ = services.AddSingleton<IDbErrorDetector, SqlServerErrorDetector>();
        _ = services.AddSingleton<IDbErrorResolver, DbErrorResolverDefault>();

        _ = services.AddScoped<IInterceptor, HashShadowPropertiesInterceptor>();

        _ = services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();


        if (!environment.IsTest())
        {
            _ = SqlPersistenceHelpers.RegisterDbContext<IdentityWriteDbContext>(
                  services,
                  config,
                  databaseSectionName,
                  DbConnectionMode.ReadWrite,
                  InfrastructureAssemblyReference.Assembly);

            _ = SqlPersistenceHelpers.RegisterDbContext<IdentityReadDbContext>(
                 services,
                 config,
                 databaseSectionName,
                 DbConnectionMode.ReadOnly,
                 InfrastructureAssemblyReference.Assembly);
        }


        return services;
    }
}
