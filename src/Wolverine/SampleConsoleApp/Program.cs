using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using JasperFx;
using JasperFx.CodeGeneration;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleConsoleApp.AppCore;
using SampleConsoleApp.Shared;
using Wolverine;
using Wolverine.Postgresql;


var builder = Host.CreateApplicationBuilder(args);
AppConfigurationHelper.ConfigureHostedAppConfiguration(
        builder,
        "Database:ER_Melksaz_Settings",
        SettingVersion.Version1);
var wolverineDbConfig = builder.Configuration.GetValue<string>("Wolverine-db");
Console.WriteLine(wolverineDbConfig);
builder.Services.AddHostedService<ServiceWorker>();
builder.Services.AddMarten(martenOptions =>
{
    martenOptions.Connection(wolverineDbConfig);
    // Optional
    martenOptions.DatabaseSchemaName = "app";

    // Optional: auto-create schema objects
    martenOptions.AutoCreateSchemaObjects = AutoCreate.All;

    martenOptions.Schema.For<InboxIdempotency>()
        .Duplicate(x => x.EventId);

    martenOptions.Schema.For<InboxIdempotency>()
        .Duplicate(x => x.LeaseTime);
});
builder.UseWolverine(opts =>
    {
        opts.UsePostgresqlPersistenceAndTransport(wolverineDbConfig).AutoProvision();

        opts.ServiceLocationPolicy = JasperFx.CodeGeneration.Model.ServiceLocationPolicy.AlwaysAllowed;

        opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Auto;
        opts.CodeGeneration.GeneratedCodeOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Internal", "Generated");
        opts.CodeGeneration.ApplicationAssembly = typeof(OrderCreatedEventHandler).Assembly;

        //opts.PublishMessage<OrderCreatedEvent>().ToRabbitExchange("sample-console-app1-jobs");
        //opts.PublishMessage<SendEmailEvent>().ToRabbitExchange("sample-console-app1-jobs");
        //opts.PublishMessage<SendSmsEvent>().ToRabbitExchange("sample-console-app1-jobs");

        opts.ConfigureMessaging(typeof(Program).Assembly, "sample-console-app1");

        opts.Policies.UseDurableLocalQueues();
        opts.Policies.UseDurableInboxOnAllListeners();
        opts.Policies
            .ForMessagesOfType<IEventMessage>()
            .AddMiddleware<IdempotencyMiddleware>();

        opts.Durability.MessageIdentity = MessageIdentity.IdAndDestination;


        // Production runs the pre-generated code with no runtime compilation.
        // AssertAllPreGeneratedTypesExist (default: false) makes a missing or stale
        // generated type fail fast at startup instead of silently misbehaving.
        opts.Services.CritterStackDefaults(x =>
        {
            x.Production.GeneratedCodeMode = TypeLoadMode.Auto;
            x.Production.AssertAllPreGeneratedTypesExist = true;
        });
    });
var app = builder.Build();

return await app.RunJasperFxCommands(args);

