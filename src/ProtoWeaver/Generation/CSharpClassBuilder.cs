using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation;

public sealed class CSharpClassBuilder
{
    public CompilationUnitSyntax CompilationUnit { get; set; } = null!;
    public BaseNamespaceDeclarationSyntax Namespace { get; set; } = null!;
    public ClassDeclarationSyntax Class { get; set; } = null!;
    public string ClassName { get; set; } = string.Empty;
    public string Build()
    {
        var ns = this.Namespace
                .AddMembers(this.Class);

        var cu = this.CompilationUnit
                .AddMembers(ns);

        return cu
           .NormalizeWhitespace()
           .ToFullString();
    }
}