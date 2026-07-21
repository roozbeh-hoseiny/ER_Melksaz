using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public abstract class MessageTypeBase : IProtoAnnotation { }
public sealed class ApiRequestMessageType : MessageTypeBase
{
    public readonly static ApiRequestMessageType Instance = new ApiRequestMessageType();
}
public sealed class ApiResponseMessageType : MessageTypeBase
{
    public readonly static ApiResponseMessageType Instance = new ApiResponseMessageType();
}
public sealed class ApiReplyMessageType : MessageTypeBase
{
    public readonly static ApiReplyMessageType Instance = new ApiReplyMessageType();
}
public sealed class GoogleMessageType : MessageTypeBase
{
    public readonly static GoogleMessageType Instance = new GoogleMessageType();
}
public sealed class SharedMessageType : MessageTypeBase
{
    public readonly static SharedMessageType Instance = new SharedMessageType();
}