using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;

namespace ProtoWeaver.Generation.Mapping.AssignmentGenerators;

internal sealed class MessageAssignmentGenerator : IAssignmentValueGenerator
{
    private readonly IMessageNameResolver _messageNameResolver;

    public MessageAssignmentGenerator(IMessageNameResolver messageNameResolver)
    {
        this._messageNameResolver = messageNameResolver;
    }

    public bool CanHandle(AssignmentGenerationContext context)
    {
        return context.Property.Message is not null;
    }

    public AssignmentExpressionSyntax Generate(AssignmentGenerationContext context)
    {
        var message = this._messageNameResolver.GetOrCreate(context.Message);

        var invocation =
            SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(message.ClassName),
                    SyntaxFactory.IdentifierName("MapToGrpcRequest")))

            .AddArgumentListArguments(

                SyntaxFactory.Argument(

                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,

                        context.SourceExpression,

                        SyntaxFactory.IdentifierName(
                            context.Property.Name))));

        return SyntaxFactory.AssignmentExpression(

            SyntaxKind.SimpleAssignmentExpression,

            SyntaxFactory.IdentifierName(
                context.TargetPropertyName),

            SyntaxFactory.PostfixUnaryExpression(
                SyntaxKind.SuppressNullableWarningExpression,
                invocation));
    }
}
