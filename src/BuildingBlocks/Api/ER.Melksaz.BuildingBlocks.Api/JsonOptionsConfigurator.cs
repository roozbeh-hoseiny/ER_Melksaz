using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ER.Melksaz.BuildingBlocks.Api;

internal class JsonOptionsConfigurator : IConfigureOptions<JsonOptions>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ApiOptions _opts;

    public JsonOptionsConfigurator(
        IServiceProvider serviceProvider,
        IOptions<ApiOptions> opts)
    {
        this._serviceProvider = serviceProvider;
        this._opts = opts.Value;
    }
    public void Configure(JsonOptions options)
    {
        options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        this.ConfigJsonConverters(options);
    }

    private void ConfigJsonConverters(JsonOptions options)
    {
        foreach (var jsonConverterType in this._opts.ApiAssembly.DefinedTypes
            .Where(type =>
                type is { IsClass: true, IsAbstract: false }
                && type.IsAssignableTo(typeof(JsonConverter))))
        {
            var converter = ActivatorUtilities.CreateInstance(this._serviceProvider, jsonConverterType);
            var jsonConverter = converter as JsonConverter;
            if (jsonConverter is null) continue;
            options.SerializerOptions.Converters.Add(jsonConverter);
        }
    }
}
public sealed class ApiOptions
{
    public Assembly ApiAssembly { get; set; } = null!;
}
