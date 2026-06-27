using ER.Melksaz.Hosting.LogEnrichers;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class LoggingExtensions
{
    public static LoggerConfiguration ConfigureAppLogging(
        this LoggerConfiguration logger,
        IConfiguration configuration,
        IServiceProvider services)
    {
        return logger
            .ReadFrom.Configuration(configuration)
            .Enrich.With(
                services.GetRequiredService<ServiceInfoEnricher>());
    }
}
