using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using ER.Melksaz.Modules.IdentityModule.Api;
using JasperFx;
using JasperFx.CodeGeneration;
using Marten;
using Scalar.AspNetCore;
using Serilog;
using Wolverine;
using Wolverine.EntityFrameworkCore;

var isCodegen = args.Contains("codegen", StringComparer.OrdinalIgnoreCase);
string wolverineDbConfig = string.Empty;
var builder = WebApplication.CreateBuilder(args);

AppConfigurationHelper.ConfigureWebAppConfiguration(
        builder,
        "Database:ER_Melksaz_Settings",
        SettingVersion.Version1);

wolverineDbConfig = builder.Configuration.GetValue<string>("Wolverine-db");

builder.Host.UseSerilog((context, serviceProvider, logger) =>
{
    logger.ConfigureAppLogging(builder.Configuration, serviceProvider);
});
/**********************************************************/
/******************** Wolverine ***************************/
/**********************************************************/
builder.Host.UseWolverine(opts =>
{
    opts.ServiceLocationPolicy = JasperFx.CodeGeneration.Model.ServiceLocationPolicy.AlwaysAllowed;

    opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
    opts.CodeGeneration.GeneratedCodeOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Internal", "Generated");
    opts.CodeGeneration.ApplicationAssembly = ApiAssemblyReference.Assembly;

    //opts.PersistMessagesWithSqlServer(wolverineDbConfig!);
    //opts.PersistMessagesWithPostgresql(wolverineDbConfig!);

    // If you're also using EF Core, you may want this as well
    opts.UseEntityFrameworkCoreTransactions();

    // Durable inbox/outbox
    opts.Policies.UseDurableLocalQueues();
    opts.Durability.KeepAfterMessageHandling = TimeSpan.FromHours(1);
    opts.LocalQueue("q1").UseDurableInbox();

    // Production runs the pre-generated code with no runtime compilation.
    // AssertAllPreGeneratedTypesExist (default: false) makes a missing or stale
    // generated type fail fast at startup instead of silently misbehaving.
    opts.Services.CritterStackDefaults(x =>
    {
        x.Production.GeneratedCodeMode = TypeLoadMode.Static;
        x.Production.AssertAllPreGeneratedTypesExist = true;
    });
});
builder.Services.AddMarten(options =>
{
    options.Connection(wolverineDbConfig);

    // Optional
    options.DatabaseSchemaName = "app";

    // Optional: auto-create schema objects
    options.AutoCreateSchemaObjects = AutoCreate.All;
})
.UseLightweightSessions();
/**********************************************************/


builder.Services.InstallApiServices(builder.Configuration, builder.Environment, ApiAssemblyReference.Assembly);
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

// Route command-line args so `dotnet run -- codegen write` (and `codegen delete`, `check-env`,
// `describe`, …) work; with no args this just runs the web host.
return await app.RunJasperFxCommands(args);


