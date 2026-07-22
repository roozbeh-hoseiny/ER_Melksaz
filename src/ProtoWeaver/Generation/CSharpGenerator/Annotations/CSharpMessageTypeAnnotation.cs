using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public interface IMessageTypeBase : IProtoAnnotation
{
    string MessageTypeName { get; }
}
public sealed class ApiRequestMessageType : IMessageTypeBase
{
    public static readonly ApiRequestMessageType Instance = new ApiRequestMessageType();

    public string MessageTypeName => "ApiRequest";
}
public sealed class ApiResponseMessageType : IMessageTypeBase
{
    public static readonly ApiResponseMessageType Instance = new ApiResponseMessageType();

    public string MessageTypeName => "ApiResponse";
}
public sealed class ApiReplyMessageType : IMessageTypeBase
{
    public static readonly ApiReplyMessageType Instance = new ApiReplyMessageType();

    public string MessageTypeName => "ApiReply";
}
public sealed class GoogleMessageType : IMessageTypeBase
{
    public static readonly GoogleMessageType Instance = new GoogleMessageType();
    public string MessageTypeName => "Google";
}
public sealed class SharedMessageType : IMessageTypeBase
{
    public static readonly SharedMessageType Instance = new SharedMessageType();
    public string MessageTypeName => "Shared";
}