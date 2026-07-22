using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoPropertyAnnotationProcessor : IProtoAnnotationProcessor<ProtoProperty>
{
    void Process(ProtoProperty property, ProtoMessage message);
}