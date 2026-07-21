using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.CSharpGenerator;

public abstract class CSharpSyntaxBuilder : ICSharpBuilder
{
    public CompilationUnitSyntax CompilationUnit { get; private set; }

    protected CSharpSyntaxBuilder()
    {
        this.CompilationUnit = SyntaxFactory.CompilationUnit();
    }

    public void UpdateCompilationUnit(Func<CompilationUnitSyntax, CompilationUnitSyntax> update)
    {
        ArgumentNullException.ThrowIfNull(update);

        this.CompilationUnit = update(this.CompilationUnit);
    }

    public void AddUsing(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        this.UpdateCompilationUnit(x =>
            x.AddUsings(
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(@namespace))));
    }

    protected static string Normalize(SyntaxNode node)
    {
        return node
            .NormalizeWhitespace()
            .ToFullString();
    }

    public abstract CompilationUnitSyntax Build();
}
