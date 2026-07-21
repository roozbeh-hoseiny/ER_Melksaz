using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMethodAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoMethod src);
}
