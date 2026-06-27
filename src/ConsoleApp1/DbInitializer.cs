using ER.Melksaz.Modules.IdentityModule.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsoleApp1;

internal static class DbInitializer
{
    public static async Task Execute(IServiceProvider serviceProvider, bool checkEnv = false)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var environment = services.GetService<IWebHostEnvironment>();

        if (environment is not null && !environment.IsDevelopment() && checkEnv)
        {
            return;
        }

        try
        {
            await IdentityPersistenceHelper.SeedAsync(serviceProvider, CancellationToken.None)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred seeding the DB.");
            throw;
        }
    }
}
