using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps;

internal sealed class ClassDeclarationStep : IProtoServiceGenerationStep
{
    public int Order => 1;

    public void Execute(ProtoService src, GenerationContext context)
    {
        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();

        if (classAnnotation is null) return;

        var builder = context.GetOrCreate(
            DocumentKeys.Service(classAnnotation.ClassName),
            $"{classAnnotation.ClassName}.g.cs",
            () => new CSharpClassBuilder());

        if (builder is null) return;

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