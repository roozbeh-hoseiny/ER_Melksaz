namespace ProtoWeaver.Models;

public sealed record CSharpProperty
{
    public required string Name { get; init; }
    public required CSharpType Type { get; init; }

    public bool IsNullable { get; init; }
    public string DefaultValue { get; init; } = "default!";
}