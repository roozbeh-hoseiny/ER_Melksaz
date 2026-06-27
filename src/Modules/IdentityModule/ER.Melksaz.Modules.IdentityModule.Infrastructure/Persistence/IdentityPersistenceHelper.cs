using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;

public static class IdentityPersistenceHelper
{
    public static async Task SeedAsync(
       IServiceProvider serviceProvider,
       CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");
        using var dbContext = services.GetRequiredService<IdentityWriteDbContext>();

        logger.LogInformation("Migrating '{schema_name}' ....", IdentityWriteDbContext.SchemaName);
        await dbContext.Database.MigrateAsync(cancellationToken);
        logger.LogInformation("Migrating '{schema_name}' done", IdentityWriteDbContext.SchemaName);

    }
}
