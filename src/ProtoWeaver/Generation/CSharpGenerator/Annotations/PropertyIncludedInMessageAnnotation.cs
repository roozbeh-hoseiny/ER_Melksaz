using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class PropertyIncludedInMessageAnnotation : IProtoAnnotation
{
    public static readonly PropertyIncludedInMessageAnnotation Instance = new();
}
