using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Processors.MessageAnnotationProcessors;

internal sealed class ApiRequestMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 3;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcRequest"))
            src.AddAnnotation(ApiRequestMessageType.Instance);
    }
}
