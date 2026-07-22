using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class CSharpPropertyAnnotation : IProtoAnnotation
{
    public required string Name { get; init; }
    public required CSharpTypeAnnotation Type { get; init; }

    public bool IsNullable { get; init; }
    public bool IsCollection { get; init; }
    public string DefaultValue { get; init; } = "default!";
}
public sealed record CSharpTypeAnnotation
{
    public required string Name { get; init; }
    public bool IsValueType { get; init; }
    public bool IsCollection { get; init; }
}
