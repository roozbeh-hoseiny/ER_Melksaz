using ER.Melksaz.BuildingBlocks.Persistence.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Helpers;

public static class SqlPersistenceHelpers
{
    private static readonly HashSet<string> RegisteredHealthCheks = [];

    public static IServiceCollection RegisterDbContext<TDbContext>(
        IServiceCollection services,
        IConfiguration config,
        string databaseSectionName,
        DbConnectionMode mode,
        Assembly migrationAssembly)
        where TDbContext : DbContext
    {
        var connectionString = PersistenceHelpers.GetConnectionstring(config, databaseSectionName, mode);
        services
            .AddDbContext<TDbContext>((sp, o) =>
            {
                o.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

                o.UseSqlServer(connectionString, opts =>
                    {
                        opts.MigrationsAssembly(migrationAssembly);
                    })
                    .UseQueryTrackingBehavior(mode.Equals(DbConnectionMode.ReadOnly)
                        ? QueryTrackingBehavior.NoTracking
                        : QueryTrackingBehavior.TrackAll);
#if DEBUG
                o.EnableSensitiveDataLogging();
#endif

                var interceptors = sp.GetServices<IInterceptor>();

                o.AddInterceptors(interceptors);
            });

        services.RegisterSqlHealthCheck(connectionString, databaseSectionName);

        return services;
    }
    private static IServiceCollection RegisterSqlHealthCheck(this IServiceCollection services, string connectionString, string name)
    {
        if (RegisteredHealthCheks.TryGetValue(name, out var x) && !string.IsNullOrWhiteSpace(x))
        {
            return services;
        }

        _ = RegisteredHealthCheks.Add(name);

        _ = services
            .AddHealthChecks()
            .AddSqlServer(
                connectionString,
                healthQuery: "SELECT GetDate();",
                name: name);
        return services;
    }
}
