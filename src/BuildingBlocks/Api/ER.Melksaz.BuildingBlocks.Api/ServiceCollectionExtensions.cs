using Asp.Versioning;
using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection InstallApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        services.Configure<ApiOptions>(opts =>
        {
            opts.ApiAssembly = assembly;
        });

        services.TryAddSingleton<IResultHandler, ResultHandlerDefault>();

        services.AddResponseCaching();

        services.AddCors();

        services.AddHttpContextAccessor();

        services.AddEndpoints(assembly);

        services.InstallApiVersioning();

        services.InstallGlobalExceptionHandler();

        services.Configure<RouteHandlerOptions>(o =>
        {
            o.ThrowOnBadRequest = true;
        });

        services.AddAntiforgery(options =>
        {
            // Angular automatically reads this cookie and sends it as header: X-XSRF-TOKEN
            options.HeaderName = "X-XSRF-TOKEN";
        });

        services.ConfigureOptions<JsonOptionsConfigurator>();

        HostedServiceServiceInstallerHelper.InstallHostedServiceServices(
                services,
                configuration,
                assembly);

        return services;
    }

    internal static IServiceCollection InstallApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("X-Version"));

        }).AddApiExplorer(options =>
        {
            // Format: v1, v2, ...
            options.GroupNameFormat = "'v'VVV";

            // Substitute version into URL
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    internal static IServiceCollection InstallGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

}
