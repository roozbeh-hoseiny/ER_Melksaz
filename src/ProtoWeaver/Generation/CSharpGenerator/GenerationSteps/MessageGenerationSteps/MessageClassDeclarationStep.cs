using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.MessageGenerationSteps;

internal sealed class MessageClassDeclarationStep : IProtoMessageGenerationStep
{
    private readonly IMessageNameResolver _messageNameResolver;

    public int Order => 1;

    public MessageClassDeclarationStep(IMessageNameResolver messageNameResolver)
    {
        this._messageNameResolver = messageNameResolver;
    }

    public void Execute(ProtoMessage src, GenerationContext context)
    {
        if (!src.CanCreateClass) return;

        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();
        var classNameInfo = this._messageNameResolver.Create(src);
        var classOutputPath = src.Annotations.Get<CSharpDocumentAnnotation>();

        if (classNameInfo is null || classAnnotation is null) return;


        var documentKey = src.GetDocumentKey();

        var builder = context.GetOrCreateDocumentBuilder(
            documentKey,
            $"{classNameInfo.ClassName}.g.cs",
            () => new CSharpClassBuilder());

        if (builder is null) return;

        if (classOutputPath is not null)
        {
            context.AddAnnotation(documentKey, classOutputPath);
        }

        builder.SetNamespace(classNameInfo.Namespace);
        builder.CreateClass(classNameInfo.ClassName, classAnnotation.Keywords);

        this._messageNameResolver.Add(new MessageKey(src.Package, src.FullName),
            new MessageName(classAnnotation.Namespace, classAnnotation.ClassName));
    }
}
