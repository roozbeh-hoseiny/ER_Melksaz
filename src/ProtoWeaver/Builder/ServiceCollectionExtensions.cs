using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation;
using ProtoWeaver.Generation.CSharpGenerator.Pipelines;
using ProtoWeaver.Generation.Mapping;

namespace ProtoWeaver.Builder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProtoWeaver(this IServiceCollection services, Action<IProtoWeaverBuilder> configure)
    {
        var builder = new ProtoWeaverBuilder(services);

        services.AddSingleton<PropertyAnnotationProcessorPipeline>();
        services.AddSingleton<MessageAnnotationProcessorPipeline>();
        services.AddSingleton<ServiceAnnotationProcessorPipeline>();


        services.AddSingleton<PropertyGenerationPipeline>();
        services.AddSingleton<MessageGenerationPipeline>();
        services.AddSingleton<ServiceGenerationPipeline>();


        services.AddSingleton<IAssignmentGeneratorResolver, DefaultAssignmentGeneratorResolver>();
        services.AddSingleton<ProtoWeaverGenerator>();

        configure(builder);

        services.AddSingleton(builder.Options);

        return services;
    }
}