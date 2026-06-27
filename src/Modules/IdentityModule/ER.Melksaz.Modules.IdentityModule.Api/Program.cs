using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using ER.Melksaz.Modules.IdentityModule.Api;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

AppConfigurationHelper.ConfigureWebAppConfiguration(
    builder,
    "Database:ER_Melksaz_Settings",
    SettingVersion.Version1);

builder.Host.UseSerilog((context, serviceProvider, logger) =>
{
    logger.ConfigureAppLogging(builder.Configuration, serviceProvider);
});
builder.Services.InstallApiServices(builder.Configuration, ApiAssemblyReference.Assembly);
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/scalar");
}
app.UseHttpsRedirection();
app.UseExceptionHandler();

await app.RunAsync();
