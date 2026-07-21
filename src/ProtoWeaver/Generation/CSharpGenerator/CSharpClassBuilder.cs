using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProtoWeaver.Generation.CSharpGenerator;

public sealed class CSharpClassBuilder : CSharpSyntaxBuilder
{
    public BaseNamespaceDeclarationSyntax? Namespace { get; private set; }
    public ClassDeclarationSyntax? Class { get; private set; }
    public string ClassName { get; private set; } = string.Empty;

    public void SetNamespace(string namespaceName)
    {
        this.Namespace =
            SyntaxFactory.FileScopedNamespaceDeclaration(
                SyntaxFactory.ParseName(namespaceName));
    }

    public void UpdateNamespace(Func<BaseNamespaceDeclarationSyntax, BaseNamespaceDeclarationSyntax> update)
    {
        ArgumentNullException.ThrowIfNull(update);

        if (this.Namespace is null)
            throw new InvalidOperationException(
                "Namespace has not been created.");

        this.Namespace = update(this.Namespace);
    }

    public void CreateClass(string className, IEnumerable<SyntaxKind> modifiers)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(className);

        var @class = SyntaxFactory.ClassDeclaration(className);

        foreach (var modifier in modifiers)
        {
            @class = @class.AddModifiers(
                SyntaxFactory.Token(modifier));
        }

        this.ClassName = className;
        this.Class = @class;
    }

    public void UpdateClass(Func<ClassDeclarationSyntax, ClassDeclarationSyntax> update)
    {
        ArgumentNullException.ThrowIfNull(update);

        if (this.Class is null)
            throw new InvalidOperationException(
                "Class has not been created yet.");

        this.Class = update(this.Class);
    }

    public override CompilationUnitSyntax Build()
    {
        if (this.Namespace is null)
            throw new InvalidOperationException();

        if (this.Class is null)
            throw new InvalidOperationException();

        return this.CompilationUnit
            .AddMembers(
                this.Namespace.AddMembers(
                    this.Class));
    }
}