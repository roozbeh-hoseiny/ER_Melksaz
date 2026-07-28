using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.AssignmentValueGenerators;

internal sealed class AnyAssignmentGenerator : IAssignmentValueGenerator
{
    public ProtoTypeKind Kind => ProtoTypeKind.Any;

    public ExpressionSyntax GenerateValue(AssignmentGenerationContext context)
    {
        throw new NotSupportedException(
            "google.protobuf.Any mapping is not implemented.");
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