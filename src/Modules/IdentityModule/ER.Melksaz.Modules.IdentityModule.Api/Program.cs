using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using ER.Melksaz.Modules.IdentityModule.Api;
using JasperFx.CodeGeneration;
using Scalar.AspNetCore;
using Serilog;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

AppConfigurationHelper.ConfigureWebAppConfiguration(
    builder,
    "Database:ER_Melksaz_Settings",
    SettingVersion.Version1);

var db = builder.Configuration.GetValue<string>("Wolverine-db");

builder.Host.UseSerilog((context, serviceProvider, logger) =>
{
    logger.ConfigureAppLogging(builder.Configuration, serviceProvider);
});

builder.Host.UseWolverine(opts =>
{
    opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;

    opts.CodeGeneration.GeneratedCodeOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Internal", "Generated");

    opts.PersistMessagesWithSqlServer(db!);

    // If you're also using EF Core, you may want this as well
    opts.UseEntityFrameworkCoreTransactions();

    opts.Policies.UseDurableLocalQueues();
    opts.Durability.KeepAfterMessageHandling = TimeSpan.FromHours(1);
    opts.LocalQueue("q1").UseDurableInbox();
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
