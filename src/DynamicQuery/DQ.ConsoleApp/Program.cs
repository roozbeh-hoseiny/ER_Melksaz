using DQ.ConsoleApp.AppCore;
using DQ.ConsoleApp.AppCore.Persistence;
using DQ.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "DefaultConnection"));
    });
builder.Services.AddDynamicQueryEntityFrameworkCore();
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<QueryTestService>();

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();

await host.RunAsync();