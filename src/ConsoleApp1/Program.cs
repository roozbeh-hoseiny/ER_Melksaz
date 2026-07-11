using ConsoleApp1;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

try
{
    var host = CreateHostBuilder(args).Build();

    await DbInitializer.Execute(host.Services, false).ConfigureAwait(false);

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
}
IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, logger) =>
        {
            logger.ConfigureAppLogging(context.Configuration, services);
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            AppConfigurationHelper.ConfigureHostedAppConfiguration(hostingContext, config, "Database:SampleAppConfig", SettingVersion.Version1);
        })
        .ConfigureServices((hostBuilderContext, services) =>
        {
            _ = HostedServiceServiceInstallerHelper.InstallHostedServiceServices(
                services,
                hostBuilderContext.Configuration,
                hostBuilderContext.HostingEnvironment,
                SampleApplicationAssemblyReference.Assembly);
        });