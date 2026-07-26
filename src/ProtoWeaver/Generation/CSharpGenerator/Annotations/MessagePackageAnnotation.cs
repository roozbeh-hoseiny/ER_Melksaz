using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

internal abstract class MessagePackageAnnotation : IProtoAnnotation;
internal sealed class GoogleProtobufMessagePackageAnnotation : MessagePackageAnnotation
{
    public static readonly GoogleProtobufMessagePackageAnnotation Instance = new();
}