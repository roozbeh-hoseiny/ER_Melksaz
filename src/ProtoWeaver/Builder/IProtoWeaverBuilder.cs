using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation.CSharpGenerator;
using System.Reflection;

namespace ProtoWeaver.Builder;

public interface IProtoWeaverBuilder
{
    IServiceCollection Services { get; }

    ProtoWeaverOptions Options { get; }

    IProtoWeaverBuilder WithWriter<TWriter>()
        where TWriter : class, ICSharpDocumentWriter;

    IProtoWeaverBuilder ScanAssembly(Assembly assembly);

    IProtoWeaverBuilder ScanAssemblies(params Assembly[] assemblies);

    IProtoWeaverBuilder AddAnnotationProcessor<T>()
        where T : class;

    IProtoWeaverBuilder AddGenerationStep<T>()
        where T : class;
}
public sealed class ProtoWeaverOptions
{
    public Type? WriterType { get; set; }
    public List<Assembly> Assemblies { get; } = [];
    public string OutputDirectory { get; set; } = "Generated";
}
public sealed class ProtoWeaverBuilder : IProtoWeaverBuilder
{
    public IServiceCollection Services { get; }
    public ProtoWeaverOptions Options { get; } = new();

    public ProtoWeaverBuilder(IServiceCollection services)
    {
        this.Services = services;
    }

    public IProtoWeaverBuilder WithWriter<TWriter>() where TWriter : class, ICSharpDocumentWriter
    {
        this.Options.WriterType = typeof(TWriter);

        this.Services.AddSingleton<ICSharpDocumentWriter, TWriter>();

        return this;
    }

    public IProtoWeaverBuilder ScanAssembly(Assembly assembly)
    {
        ProtoWeaverAssemblyScanner.Scan(
            this.Services,
            assembly);

        this.Options.Assemblies.Add(assembly);

        return this;
    }

    public IProtoWeaverBuilder ScanAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            this.ScanAssembly(assembly);

        return this;
    }

    public IProtoWeaverBuilder AddAnnotationProcessor<T>()
        where T : class
    {
        this.Services.AddSingleton(typeof(T));

        return this;
    }

    public IProtoWeaverBuilder AddGenerationStep<T>()
        where T : class
    {
        this.Services.AddSingleton(typeof(T));

        return this;
    }
}
internal static class ProtoWeaverAssemblyScanner
{
    public static void Scan(IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                RegisterAnnotationProcessors(
                    services,
                    type);

                RegisterGenerationSteps(
                    services,
                    type);
            }
        }
    }

    private static void RegisterAnnotationProcessors(
        IServiceCollection services,
        Type type)
    {
        //foreach (var i in type.GetInterfaces())
        //{
        //    if (!i.IsGenericType)
        //        continue;

        //    if (i.GetGenericTypeDefinition() != typeof(IProtoAnnotationProcessor<>))
        //        continue;

        //    services.AddSingleton(i, type);
        //}
    }

    private static void RegisterGenerationSteps(
        IServiceCollection services,
        Type type)
    {
        //foreach (var i in type.GetInterfaces())
        //{
        //    if (!i.IsGenericType)
        //        continue;

        //    if (i.GetGenericTypeDefinition() !=
        //        typeof(IGenerationStep<>))
        //        continue;

        //    services.AddSingleton(i, type);
        //}
    }
}
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProtoWeaver(this IServiceCollection services, Action<IProtoWeaverBuilder> configure)
    {
        var builder = new ProtoWeaverBuilder(services);

        configure(builder);

        services.AddSingleton(builder.Options);

        return services;
    }
}