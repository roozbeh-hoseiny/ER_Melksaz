using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal sealed class SharedMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 5;

    public void Process(ProtoMessage src)
    {
        if (src.Package.Equals("ER.Sanjesh.SharedGrpcMessages"))
            src.AddAnnotation(SharedMessageType.Instance);

    }
}