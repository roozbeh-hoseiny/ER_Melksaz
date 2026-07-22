using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.MessageGenerationSteps;

internal sealed class MessageClassDeclarationStep : IProtoMessageGenerationStep
{
    public int Order => 1;

    public void Execute(ProtoMessage src, GenerationContext context)
    {
        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();
        var classOutputPath = src.Annotations.Get<CSharpDocumentAnnotation>();

        if (classAnnotation is null) return;
        var documentKey = src.GetDocumentKey();

        var builder = context.GetOrCreateDocumentBuilder(
            documentKey,
            $"{classAnnotation.ClassName}.g.cs",
            () => new CSharpClassBuilder());

        if (builder is null) return;

        if (classOutputPath is not null)
        {
            context.AddAnnotation(documentKey, classOutputPath);
        }

        builder.SetNamespace(classAnnotation.Namespace);
        builder.CreateClass(classAnnotation.ClassName, classAnnotation.Keywords);
    }
}