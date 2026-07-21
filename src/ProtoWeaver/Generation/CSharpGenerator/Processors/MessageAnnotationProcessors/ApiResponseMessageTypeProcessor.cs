using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Processors.MessageAnnotationProcessors;

internal sealed class ApiResponseMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 4;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcResponse"))
            src.AddAnnotation(ApiResponseMessageType.Instance);
    }
}
