using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Processors;

internal sealed class GoogleMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 1;

    public void Process(ProtoMessage src)
    {
        if (src.Package.Equals("google.protobuf"))
            src.AddAnnotation(GoogleMessageType.Instance);

    }
}
internal sealed class SharedMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 2;

    public void Process(ProtoMessage src)
    {
        if (src.Package.Equals("ER.Sanjesh.SharedGrpcMessages"))
            src.AddAnnotation(SharedMessageType.Instance);

    }
}
internal sealed class ApiRequestMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 3;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcRequest"))
            src.AddAnnotation(ApiRequestMessageType.Instance);
    }
}
internal sealed class ApiResponseMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 4;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcResponse"))
            src.AddAnnotation(ApiResponseMessageType.Instance);
    }
}
internal sealed class ApiReplyMessageTypeProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => 5;

    public void Process(ProtoMessage src)
    {
        if (src.FullName.EndsWith("_GrpcReply"))
            src.AddAnnotation(ApiReplyMessageType.Instance);
    }
}