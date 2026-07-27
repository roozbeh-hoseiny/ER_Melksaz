using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal sealed class ApiReplyMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 3;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcReply"))
            src.AddAnnotation(ApiReplyMessageType.Instance);
    }
}