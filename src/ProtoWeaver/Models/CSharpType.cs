namespace ProtoWeaver.Models;

public sealed record CSharpType
{
    public required string Name { get; init; }
    public bool IsValueType { get; init; }
    public bool IsCollection { get; init; }
}
