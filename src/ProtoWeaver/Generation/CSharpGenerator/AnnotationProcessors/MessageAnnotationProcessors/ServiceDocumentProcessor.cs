using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

internal sealed class MessageDocumentProcessor : IProtoMessageAnnotationProcessor
{
    public int Order => int.MaxValue;

    public void Process(ProtoMessage src)
    {
        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();
        var servicveNameAnnotation = src.Annotations.Get<ServiceNameAnnotation>();

        if (classAnnotation is null) return;
        if (servicveNameAnnotation is null) return;

        src.Annotations.Add(
            new CSharpDocumentAnnotation()
            {
                FileName = $"{classAnnotation.ClassName}.g.cs",
                RelativePath = $"services/{servicveNameAnnotation.Name}/Contracts"
            });
    }
}