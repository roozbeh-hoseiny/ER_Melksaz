using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.Contracts;

public interface IAssignmentValueGenerator
{
    ProtoTypeKind Kind { get; }

    ExpressionSyntax GenerateValue(AssignmentGenerationContext context);
    ExpressionSyntax GenerateRepeatedValue(AssignmentGenerationContext context);
}
