using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.AssignmentValueGenerators;

internal sealed class EnumAssignmentGenerator
    : IAssignmentValueGenerator
{
    public ProtoTypeKind Kind => ProtoTypeKind.Enum;

    public ExpressionSyntax GenerateValue(AssignmentGenerationContext context)
    {
        return SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            context.SourceExpression,
            SyntaxFactory.IdentifierName(
                context.SourcePropertyName));
    }
    public ExpressionSyntax GenerateRepeatedValue(AssignmentGenerationContext context)
    {
        return CreateRepeatedExpression(
            context,
            SyntaxFactory.IdentifierName("x"));
    }

    private static ExpressionSyntax CreateRepeatedExpression(
        AssignmentGenerationContext context,
        ExpressionSyntax selector)
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
                            selector))),

                SyntaxFactory.IdentifierName("ToList")));
    }
    private static MemberAccessExpressionSyntax SourceProperty(AssignmentGenerationContext context)
    {
        return SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            context.SourceExpression,
            SyntaxFactory.IdentifierName(
                context.SourcePropertyName));
    }
}
