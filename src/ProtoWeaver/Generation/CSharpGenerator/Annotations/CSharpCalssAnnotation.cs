namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;
public sealed class CSharpClassAnnotation : IProtoAnnotation
{
    public required string Namespace { get; init; }
    public required string ClassName { get; init; }
    public required string ClassDefinition { get; init; }
}
