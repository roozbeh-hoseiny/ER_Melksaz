using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.AssignmentValueGenerators;

internal sealed class GeneratedMessageAssignmentGenerator : IAssignmentValueGenerator
{
    private readonly IMessageNameResolver _messageNameResolver;

    public ProtoTypeKind Kind => ProtoTypeKind.GeneratedMessage;

    public GeneratedMessageAssignmentGenerator(IMessageNameResolver messageNameResolver)
    {
        this._messageNameResolver = messageNameResolver;
    }

    public ExpressionSyntax GenerateValue(
        AssignmentGenerationContext context)
    {
        return this.CreateMapInvocation(
            context,
            SourceProperty(context));
    }

    public ExpressionSyntax GenerateRepeatedValue(
        AssignmentGenerationContext context)
    {
        return SyntaxFactory.InvocationExpression(
            SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,

                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SourceProperty(context),
                        SyntaxFactory.IdentifierName("Select")))
                .AddArgumentListArguments(
                    SyntaxFactory.Argument(
                        SyntaxFactory.SimpleLambdaExpression(
                            SyntaxFactory.Parameter(
                                SyntaxFactory.Identifier("x")),
                            this.CreateMapInvocation(
                                context,
                                SyntaxFactory.IdentifierName("x"))))),

                SyntaxFactory.IdentifierName("ToList")));
    }

    private ExpressionSyntax CreateMapInvocation(
        AssignmentGenerationContext context,
        ExpressionSyntax source)
    {
        var api =
            this._messageNameResolver.GetRequired(
                context.Property.Message!);

        return SyntaxFactory.PostfixUnaryExpression(
            SyntaxKind.SuppressNullableWarningExpression,

            SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(api.ClassName),
                    SyntaxFactory.IdentifierName(
                        "MapToGrpcRequest")))
            .AddArgumentListArguments(
                SyntaxFactory.Argument(source)));
    }

    private static MemberAccessExpressionSyntax SourceProperty(
        AssignmentGenerationContext context)
    {
        return SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            context.SourceExpression,
            SyntaxFactory.IdentifierName(
                context.SourcePropertyName));
    }
}