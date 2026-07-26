using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation;

internal static class AssignmentValueGeneratorExtensions
{
    public static AssignmentExpressionSyntax Generate(
        this IAssignmentValueGenerator generator,
        AssignmentGenerationContext context)
    {
        return SyntaxFactory.AssignmentExpression(
            SyntaxKind.SimpleAssignmentExpression,
            context.TargetExpression,
            generator.GenerateValue(context));
    }
}