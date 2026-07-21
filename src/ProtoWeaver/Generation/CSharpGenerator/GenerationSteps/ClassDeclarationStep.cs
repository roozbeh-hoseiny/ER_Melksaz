using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps;

internal sealed class ClassDeclarationStep : IProtoServiceGenerationStep
{
    public int Order => 1;

    public void Execute(ProtoService src, CSharpClassBuilder builder)
    {
        var classAnnotation = src.Annotations.Get<CSharpClassAnnotation>();

        if (classAnnotation is null) return;

        builder.ClassName = classAnnotation.ClassName;

        var @namespace = SyntaxFactory.FileScopedNamespaceDeclaration(
                SyntaxFactory.ParseName(classAnnotation.Namespace));

        var @class = SyntaxFactory.ClassDeclaration(classAnnotation.ClassName);
        foreach (var keyword in classAnnotation.Keywords)
        {
            @class = @class.AddModifiers(
                SyntaxFactory.Token(keyword));
        }

        builder.CompilationUnit ??= SyntaxFactory.CompilationUnit();
        builder.Namespace = @namespace;
        builder.Class = @class;
    }
}