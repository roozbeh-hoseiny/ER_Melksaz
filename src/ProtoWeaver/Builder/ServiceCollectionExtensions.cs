using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation;
using ProtoWeaver.Generation.CSharpGenerator.Pipelines;

namespace ProtoWeaver.Builder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProtoWeaver(this IServiceCollection services, Action<IProtoWeaverBuilder> configure)
    {
        var builder = new ProtoWeaverBuilder(services);

        services.AddSingleton<MessageGenerationPipeline>();
        services.AddSingleton<ServiceGenerationPipeline>();
        services.AddSingleton<ServiceAnnotationProcessorPipeline>();
        services.AddSingleton<MessageAnnotationProcessorPipeline>();
        services.AddSingleton<PropertyAnnotationProcessorPipeline>();
        services.AddSingleton<ProtoWeaverGenerator>();

        configure(builder);

        services.AddSingleton(builder.Options);

        return services;
    }
}