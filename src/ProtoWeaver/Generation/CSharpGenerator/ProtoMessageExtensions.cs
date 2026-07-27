using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator;

public static class ProtoMessageExtensions
{
    extension(ProtoMessage src)
    {
        public bool IsSharedMessage => src.Annotations.Has<SharedMessageType>();
        public string MessageTypeName => string.Join('_', src.Annotations
            ?.GetAllDerived<IMessageTypeBase>()
            ?.OrderBy(x => x.MessageTypeName)
            ?.Select(x => x.MessageTypeName ?? string.Empty)
            ?.Where(x => !string.IsNullOrWhiteSpace(x)) ?? []) ?? string.Empty;
        public bool CanCreateClass => src.Annotations.Get<CSharpClassAnnotation>() is not null;
        public DocumentKey GetDocumentKey()
        {
            var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();

            if (classAnnotation is null) throw new InvalidOperationException("Can not create document key.");

            return DocumentKeys.Message($"{src.MessageTypeName}_{classAnnotation.ClassName}");
        }
    }
}
