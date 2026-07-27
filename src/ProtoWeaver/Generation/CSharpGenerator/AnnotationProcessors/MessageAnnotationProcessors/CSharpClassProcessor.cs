using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal abstract class CSharpMessageClassDefinitionProcessor<TMessageType>
    : IProtoMessageAnnotationProcessor
    where TMessageType : IMessageTypeBase
{
    public int Order => 100;


    public void Process(ProtoMessage src)
    {
        // اگر قبلا پردازش شده است، دیگر پردازش نکن
        if (src.Annotations.Has<CSharpClassAnnotation>()) return;

        var serviceNameAnnotation = src.Annotations.Get<ServiceNameAnnotation>();

        if (serviceNameAnnotation is null)
            throw new MissingAnnotationException(typeof(ServiceNameAnnotation));

        if (!src.Annotations.Has<TMessageType>()) return;

        var name = src.Name;
        var parts = name.Split('_');
        if (!src.IsSharedMessage)
        {
            if (parts.Length >= 2)
            {
                name = $"{string.Join(string.Empty, parts[..^1])}";
            }
        }
        else
        {
            parts = parts.Where(x =>
                !x.Equals("GrpcRequest", StringComparison.InvariantCultureIgnoreCase)
                && !x.Equals("GrpcResponse", StringComparison.InvariantCultureIgnoreCase)
                && !x.Equals("GrpcReply", StringComparison.InvariantCultureIgnoreCase)).ToArray();

            name = string.Join('_',
                parts.Select((p, i) => i > 0
                    ? p.StartsWith("grpc", StringComparison.InvariantCultureIgnoreCase)
                        ? p.Substring(4)
                        : p
                    : p));
        }
        var messagePostfix = string.Empty;

        if (src.Annotations.Has<ApiRequestMessageType>()) messagePostfix = "ApiRequest";
        else if (src.Annotations.Has<ApiResponseMessageType>()) messagePostfix = "ApiResponse";
        else if (src.Annotations.Has<ApiReplyMessageType>()) messagePostfix = "Reply";

        var annotation = new CSharpClassAnnotation()
        {
            ClassName = $"{name}{messagePostfix}",
            Namespace = AnnotationHelpers.GetPresentationMessageNamespace(serviceNameAnnotation.Name)
        };
        annotation.AddKeyword(SyntaxKind.PublicKeyword);
        annotation.AddKeyword(SyntaxKind.SealedKeyword);

        src.AddAnnotation(annotation);
    }
}
internal sealed class CSharpApiRequestMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiRequestMessageType>;
internal sealed class CSharpApiResponseMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiResponseMessageType>;
internal sealed class CSharpApiReplyMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<ApiReplyMessageType>;
internal sealed class CSharpSharedMessageClassDefinitionProcessor : CSharpMessageClassDefinitionProcessor<SharedMessageType>;
