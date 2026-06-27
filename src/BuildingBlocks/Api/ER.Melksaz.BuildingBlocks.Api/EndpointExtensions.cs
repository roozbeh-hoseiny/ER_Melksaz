
using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using ER.Melksaz.BuildingBlocks.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type =>
                ServiceDescriptor.Describe(
                    typeof(IEndpoint),
                    type,
                    serviceLifetime));

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app) => MapEndpoints(app, [new ApiVersion(1)]);

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        ApiVersion[] apiVersions)
    {
        var group = CreateApiVersioning(app, apiVersions);

        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(group);
        }

        return app;
    }

    private static RouteGroupBuilder CreateApiVersioning(WebApplication app,
        ApiVersion[] apiVersions)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersions(apiVersions)
            .Build();

        return app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
    }
}