using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal abstract class CSharpMessageClassDefinitionProcessor<TMessageType>
    : IProtoMessageAnnotationProcessor
    where TMessageType : MessageTypeBase
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

        var messageTypeAnnotation = src.Annotations.GetDerived<MessageTypeBase>();

        if (messageTypeAnnotation is not TMessageType && !src.IsSharedMessage) return;

        src.Annotations.Add(new CSharpClassAnnotation()
        {
            ClassName = $"{AnnotationHelpers.RemoveLastPart(src.Name)}{this._messagePostfix}",
            Namespace = AnnotationHelpers.GetPresentationMessageNamespace(serviceNameAnnotation.Name)
        });
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
