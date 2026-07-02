using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;

public static class IdentityMigrator
{
    public static async Task MigrateAsync(IServiceProvider sp, CancellationToken ct = default)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IdentityWriteDbContext>();

        await db.Database.MigrateAsync(ct);
    }
}
