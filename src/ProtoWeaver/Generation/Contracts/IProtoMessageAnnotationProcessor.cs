using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMessageAnnotationProcessor : IProtoAnnotationProcessor<ProtoMessage>
{
    void Process(ProtoMessage src);
}