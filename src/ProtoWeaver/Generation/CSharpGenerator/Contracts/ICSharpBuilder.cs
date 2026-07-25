using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.CSharpGenerator.Contracts;

public interface ICSharpBuilder
{
    CompilationUnitSyntax Build();
}