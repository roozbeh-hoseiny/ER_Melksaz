using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

internal sealed class PropertyNameAnnotation : IProtoAnnotation
{
    public required string Name { get; init; }
}
