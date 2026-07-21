using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoPropertyAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoProperty src);
}
