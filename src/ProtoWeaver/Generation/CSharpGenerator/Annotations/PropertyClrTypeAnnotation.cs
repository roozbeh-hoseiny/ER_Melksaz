using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

internal sealed class PropertyClrTypeAnnotation : IProtoAnnotation
{
    public required string ClrType { get; init; }
    public string ClrTypeNamespace { get; set; } = string.Empty;
}
