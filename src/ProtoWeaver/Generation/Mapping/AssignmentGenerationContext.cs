using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Mapping;

public sealed class AssignmentGenerationContext
{
    public required ProtoMessage Message { get; init; }
    public required ProtoProperty Property { get; init; }

    /// <summary>
    /// مثلا src
    /// </summary>
    public required ExpressionSyntax SourceExpression { get; init; }

    /// <summary>
    /// مثلا ResourceId
    /// </summary>
    public required string TargetPropertyName { get; init; }
}
