using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class ServiceNameAnnotation : IProtoAnnotation
{
    public required string Name { get; init; }
}