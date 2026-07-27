using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal sealed class DTOMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 6;

    public void Process(ProtoMessage src)
    {
        if (
            src.Annotations.Has<ApiRequestMessageType>()
            || src.Annotations.Has<ApiResponseMessageType>()
            || src.Annotations.Has<ApiReplyMessageType>()
        )
        {
            return;
        }

        if (!src.Name.StartsWith("grpc", StringComparison.InvariantCultureIgnoreCase)) return;
    }
}