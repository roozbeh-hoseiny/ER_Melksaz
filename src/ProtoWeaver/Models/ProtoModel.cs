using Google.Protobuf.Reflection;
using ProtoWeaver.Generation;

namespace ProtoWeaver.Models;
public sealed class ProtoModel
{
    public Dictionary<string, ProtoMessage> Messages { get; } = new();
    public List<ProtoService> Services { get; } = [];
}
public abstract class AnnotatableBase : IAnnotatable
{
    public AnnotationCollection Annotations { get; } = new();

    public void AddAnnotation<T>(T annotation) where T : class, IProtoAnnotation
    {
        this.Annotations.Add(annotation);
    }
}
public sealed class ProtoService : AnnotatableBase
{
    public required string Name { get; init; }
    public required string Package { get; init; }
    public List<ProtoMethod> Methods { get; } = [];
}
public sealed class ProtoMethod : AnnotatableBase
{
    public required string Name { get; init; }
    public required ProtoMessage Request { get; init; }
    public required ProtoMessage Response { get; init; }
    public bool ClientStreaming { get; init; }
    public bool ServerStreaming { get; init; }
}
public sealed class ProtoMessage : AnnotatableBase
{
    public required string Name { get; init; }
    public required string FullName { get; init; }
    public required string Package { get; init; }
    public required string FileName { get; init; }
    public List<ProtoProperty> Properties { get; } = [];
}
public sealed class ProtoProperty : AnnotatableBase
{
    public required string Name { get; init; }
    public required string ProtoName { get; init; }
    public required FieldType FieldType { get; init; }
    public bool IsRepeated { get; init; }
    public bool IsNullable { get; init; }
    public bool IsMessage { get; init; }
    public bool IsEnum { get; init; }
    public bool IsPrimitive { get; init; }
    public ProtoMessage? Message { get; set; }
    public string? EnumName { get; init; }
}
