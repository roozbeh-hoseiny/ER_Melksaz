using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal sealed class GoogleMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 1;

    public void Process(ProtoMessage src)
    {
        if (src.Package.Equals("google.protobuf"))
            src.AddAnnotation(GoogleMessageType.Instance);

    }
}
