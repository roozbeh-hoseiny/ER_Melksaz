using Google.Protobuf.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.Mapping.AssignmentGenerators;

internal sealed class EnumAssignmentGenerator : IAssignmentValueGenerator
{
    public bool CanHandle(
        AssignmentGenerationContext context)
    {
        return context.Property.FieldType == FieldType.Enum;
    }

    public AssignmentExpressionSyntax Generate(
        AssignmentGenerationContext context)
    {
        return SyntaxFactory.AssignmentExpression(

            SyntaxKind.SimpleAssignmentExpression,

            SyntaxFactory.IdentifierName(
                context.TargetPropertyName),

            SyntaxFactory.MemberAccessExpression(

                SyntaxKind.SimpleMemberAccessExpression,

                context.SourceExpression,

                SyntaxFactory.IdentifierName(
                    context.Property.Name)));
    }
}