using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.MessageGenerationSteps;

internal sealed class MessagePropertiesGenerationStep : IProtoMessageGenerationStep
{
    private readonly IMessageNameResolver _messageNameResolver;

    public int Order => 2;

    public MessagePropertiesGenerationStep(IMessageNameResolver messageNameResolver)
    {
        this._messageNameResolver = messageNameResolver;
    }

    public void Execute(ProtoMessage src, GenerationContext context)
    {
        if (!src.CanCreateClass) return;

        var messageDocumentKey = src.GetDocumentKey();

        var builder = context.GetBuilder<CSharpClassBuilder>(messageDocumentKey);

        foreach (var property in src.Properties)
        {
            var propertySyntax = CreateProperty(
                   builder,
                   property);
            if (propertySyntax is null) continue;

            builder.UpdateClass(cls =>
               cls.AddMembers(
                   propertySyntax));
        }
    }

    private static PropertyDeclarationSyntax? CreateProperty(
        CSharpClassBuilder builder,
        ProtoProperty property)
    {
        var cSharpPropertyAnnotation = property.Annotations.Get<CSharpPropertyAnnotation>();
        var propertyClrTypeAnnotation = property.Annotations.Get<PropertyClrTypeAnnotation>();
        var propertyIsIncludedAnnotation = property.Annotations.Get<PropertyIncludedInMessageAnnotation>();
        var propertyNameAnnotation = property.Annotations.Get<PropertyNameAnnotation>();

        if (cSharpPropertyAnnotation is null) return null;
        if (propertyClrTypeAnnotation is null) return null;
        if (propertyIsIncludedAnnotation is null) return null;
        if (propertyNameAnnotation is null) return null;


        if (!string.IsNullOrWhiteSpace(propertyClrTypeAnnotation.ClrTypeNamespace))
        {
            builder.AddUsing(propertyClrTypeAnnotation.ClrTypeNamespace);
        }
        return PropertyDeclaration(
                ParseTypeName(propertyClrTypeAnnotation.ClrType),
                Identifier(propertyNameAnnotation.Name))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
            .WithInitializer(
                EqualsValueClause(
                    ParseExpression(cSharpPropertyAnnotation.DefaultValue)))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }
}