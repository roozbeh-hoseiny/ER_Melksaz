using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.ServiceGenerationSteps;

internal sealed class ServiceClassDeclarationStep : IProtoServiceGenerationStep
{
    public int Order => 1;

    public void Execute(ProtoService src, GenerationContext context)
    {
        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();
        var classOutputPath = src.Annotations.Get<CSharpDocumentAnnotation>();

        if (classAnnotation is null) return;

        var documentKey = DocumentKeys.Service(classAnnotation.ClassName);

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
/*
    // Add new Property
    builder.UpdateClass(cls => cls.AddMembers(propertySyntax));

    // Add new Methods    
    builder.UpdateClass(cls =>cls.AddMembers(methodSyntax));

    // Add New attribute
    builder.UpdateClass(cls =>cls.AddAttributeLists(attributeList));
    
    // Add new interface
    builder.UpdateClass(cls =>
        cls.AddBaseListTypes(
            SyntaxFactory.SimpleBaseType(
                SyntaxFactory.ParseTypeName("IMyService"))));

    // add using    
    builder.AddUsing("Microsoft.AspNetCore.Mvc");
*/