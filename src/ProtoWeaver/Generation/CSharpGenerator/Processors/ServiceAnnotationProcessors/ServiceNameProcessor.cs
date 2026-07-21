using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Processors.ServiceAnnotationProcessors;

internal sealed class ServiceNameProcessor : IProtoServiceAnnotationProcessor
{
    public int Order => 2;

    public void Process(ProtoService src)
    {
        src.AddAnnotation(new ServiceNameAnnotation() { Name = src.Package.Split('.')[2] });

    }
}
