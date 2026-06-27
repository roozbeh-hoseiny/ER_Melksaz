using ER.Melksaz.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace ER.Melksaz.Hosting.LogEnrichers;

internal class ServiceInfoEnricher : ILogEventEnricher
{
    private readonly IConfiguration _configuration;

    public ServiceInfoEnricher(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var serviceName = ServiceInfoHelper.GetEntryProjectName(this._configuration);
        var envName = ServiceInfoHelper.GetServiceEnvName(this._configuration);
        var prop = propertyFactory.CreateProperty("service_name", serviceName);
        var envProp = propertyFactory.CreateProperty("env_name", envName);

        logEvent.AddPropertyIfAbsent(prop);
        logEvent.AddPropertyIfAbsent(envProp);
    }


}