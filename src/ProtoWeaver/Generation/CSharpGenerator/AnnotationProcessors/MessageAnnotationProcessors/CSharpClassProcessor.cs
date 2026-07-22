using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal abstract class CSharpMessageClassDefinitionProcessor<TMessageType>
    : IProtoMessageAnnotationProcessor
    where TMessageType : IMessageTypeBase
{
    private readonly string _messagePostfix;

    public int Order => 100;

    protected CSharpMessageClassDefinitionProcessor(string messagePostfix)
    {
        this._messagePostfix = messagePostfix;
    }

    public void Process(ProtoMessage src)
    {
        var serviceNameAnnotation = src.Annotations.Get<ServiceNameAnnotation>();

        if (serviceNameAnnotation is null)
            throw new MissingAnnotationException(typeof(ServiceNameAnnotation));

        var messageTypeAnnotation = src.Annotations.GetDerived<IMessageTypeBase>();

        if (messageTypeAnnotation is not TMessageType && !src.IsSharedMessage) return;

        var annotation = new CSharpClassAnnotation()
        {
            ClassName = $"{AnnotationHelpers.RemoveLastPart(src.Name)}{this._messagePostfix}",
            Namespace = AnnotationHelpers.GetPresentationMessageNamespace(serviceNameAnnotation.Name)
        };
        annotation.AddKeyword(SyntaxKind.PublicKeyword);
        annotation.AddKeyword(SyntaxKind.SealedKeyword);

        src.AddAnnotation(annotation);
    }
}
internal sealed class CSharpApiRequestMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiRequestMessageType>
{
    public CSharpApiRequestMessageClassDefinitionProcessor() : base("ApiRequest")
    {

    }
}
internal sealed class CSharpApiResponseMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiResponseMessageType>
{
    public CSharpApiResponseMessageClassDefinitionProcessor() : base("ApiResponse")
    {

    }
}
internal sealed class CSharpApiReplyMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiReplyMessageType>
{
    public CSharpApiReplyMessageClassDefinitionProcessor() : base("Reply")
    {

    }
}

public interface IMessageNameResolver
{
    void Add(MessageKey key, MessageName value);
    MessageName? GetOrCreate(ProtoMessage message);
    MessageName? Get(MessageKey key);
    MessageName? Create(ProtoMessage message);
    MessageName GetRequired(ProtoMessage message);

}
public readonly record struct MessageKey(string Package, string Fullname);
public sealed record MessageName(string Namespace, string ClassName);

internal sealed class MessageNameResolver : IMessageNameResolver
{
    private static readonly Dictionary<MessageKey, MessageName> _messages = [];
    public void Add(MessageKey key, MessageName value)
    {
        _messages.TryAdd(key, value);
    }

    public MessageName? Get(MessageKey key)
    {
        if (!_messages.TryGetValue(key, out var result)) return null;
        return result;
    }
    public MessageName? Create(ProtoMessage message)
    {
        var classAnnotation = message.Annotations.Get<CSharpClassAnnotation>();

        if (classAnnotation is null) return null;

        return new MessageName(classAnnotation.Namespace, classAnnotation.ClassName);
    }

    public MessageName? GetOrCreate(ProtoMessage message)
    {
        var result = this.Get(new MessageKey(
            message.Package,
            message.FullName));
        return result ?? this.Create(message);
    }

    public MessageName GetRequired(ProtoMessage message)
    {
        return this.GetOrCreate(message)
            ?? throw new InvalidOperationException(
                $"No C# type mapping has been registered for proto message '{message.FullName}'.");
    }
}