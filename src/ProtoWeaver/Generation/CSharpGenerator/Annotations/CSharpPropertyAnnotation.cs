using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

public sealed class CSharpPropertyAnnotation : IProtoAnnotation
{
    public string AccessModifier { get; set; } = "public";
    public string CLRType { get; set; } = string.Empty;
    public bool IsNuallable { get; set; } = false;
    public string DefaultValue { get; set; } = string.Empty;
}
