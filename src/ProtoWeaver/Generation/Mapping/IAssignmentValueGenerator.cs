using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.Mapping;

public interface IAssignmentValueGenerator
{
    bool CanHandle(AssignmentGenerationContext context);

    AssignmentExpressionSyntax Generate(AssignmentGenerationContext context);
}