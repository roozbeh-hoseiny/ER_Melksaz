using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class CSharpDocumentAnnotation : IProtoAnnotation
{
    public string? RelativePath { get; init; }
    public string? FileName { get; init; }
    public bool Overwrite { get; init; } = true;
}