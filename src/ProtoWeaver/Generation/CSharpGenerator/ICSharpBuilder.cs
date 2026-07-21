using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.CSharpGenerator;

public interface ICSharpBuilder
{
    CompilationUnitSyntax Build();
}