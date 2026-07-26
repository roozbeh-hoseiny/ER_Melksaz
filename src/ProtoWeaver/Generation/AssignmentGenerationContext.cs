using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

public sealed class AssignmentGenerationContext
{
    public required ProtoMessage Message { get; init; }
    public required ProtoProperty Property { get; init; }
    public required ExpressionSyntax SourceExpression { get; init; }
    public required ExpressionSyntax TargetExpression { get; init; }
    public required string SourcePropertyName { get; init; }
    public required string TargetPropertyName { get; init; }
    public required ProtoTypeKind TypeKind { get; init; }
}
