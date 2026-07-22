using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;
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
            var propertyClrAnnotation = property.Annotations.Get<CSharpPropertyAnnotation>();

            if (propertyClrAnnotation is null) continue;

            builder.UpdateClass(cls =>
               cls.AddMembers(this.CreateProperty(
                   builder,
                   property,
                   propertyClrAnnotation)));
        }
    }
    private PropertyDeclarationSyntax CreateProperty(
        CSharpClassBuilder builder,
        ProtoProperty property,
        CSharpPropertyAnnotation annotation)
    {
        var typeName = annotation.Type.Name;

        if (!annotation.Type.IsValueType && property.Message is not null)
        {
            var message = this._messageNameResolver.GetOrCreate(property.Message);

            if (message is not null)
            {
                builder.AddUsing(message.Namespace);

                typeName = message.ClassName;
            }
            else
            {
                typeName = CSharpPropertyResolver.Resolve(property).Type.Name;
            }
        }

        if (annotation.Type.IsCollection)
        {
            typeName = $"IReadOnlyList<{typeName}>";
        }

        if (annotation.IsNullable && annotation.Type.IsValueType)
        {
            typeName += "?";
        }
        else if (annotation.IsNullable)
        {
            typeName += "?";
        }

        return PropertyDeclaration(
                ParseTypeName(typeName),
                Identifier(annotation.Name))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
            .WithInitializer(
                EqualsValueClause(
                    ParseExpression(annotation.DefaultValue)))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }
}