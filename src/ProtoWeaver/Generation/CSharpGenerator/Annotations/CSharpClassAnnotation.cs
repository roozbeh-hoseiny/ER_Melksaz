using Microsoft.CodeAnalysis.CSharp;
using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class CSharpClassAnnotation : IProtoAnnotation
{
    private readonly HashSet<SyntaxKind> _keywords = [];

    public required string Namespace { get; init; }
    public required string ClassName { get; init; }
    public IReadOnlyCollection<SyntaxKind> Keywords => this._keywords.ToArray().AsReadOnly();

    public void AddKeyword(SyntaxKind value) => this._keywords.Add(value);
}
