using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation.CSharpGenerator;
using System.Reflection;

namespace ProtoWeaver.Builder;

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
