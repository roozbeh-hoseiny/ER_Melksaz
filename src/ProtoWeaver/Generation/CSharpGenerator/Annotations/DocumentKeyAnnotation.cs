using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

internal class DocumentKeyAnnotation : IProtoAnnotation
{
    public required DocumentKey Key { get; init; }
}
