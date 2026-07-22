using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMethodAnnotationProcessor : IProtoAnnotationProcessor<ProtoMethod>
{
    void Process(ProtoMethod src);
}
