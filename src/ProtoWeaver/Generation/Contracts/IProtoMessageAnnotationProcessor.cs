using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMessageAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoMessage src);
}
