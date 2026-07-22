using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation.CSharpGenerator;
using System.Reflection;

namespace ProtoWeaver.Builder;

public interface IProtoWeaverBuilder
{
    IServiceCollection Services { get; }

    ProtoWeaverOptions Options { get; }

    IProtoWeaverBuilder WithWriter<TWriter>() where TWriter : class, ICSharpDocumentWriter;

    IProtoWeaverBuilder ScanAssembly(Assembly assembly);

    IProtoWeaverBuilder ScanAssemblies(params Assembly[] assemblies);

    IProtoWeaverBuilder AddAnnotationProcessor<T>() where T : class;

    IProtoWeaverBuilder AddGenerationStep<T>() where T : class;
}
