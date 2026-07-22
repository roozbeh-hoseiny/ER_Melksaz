using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors;

internal sealed class ServiceNameProcessor :
    IProtoServiceAnnotationProcessor,
    IProtoMessageAnnotationProcessor

{
    public int Order => 0;

    public void Process(ProtoService src)
    {
        src.AddAnnotation(new ServiceNameAnnotation() { Name = AnnotationHelpers.GetServiceName(src.Package) });
    }

    public void Process(ProtoMessage src)
    {
        src.AddAnnotation(new ServiceNameAnnotation() { Name = AnnotationHelpers.GetServiceName(src.Package) });
    }
}
