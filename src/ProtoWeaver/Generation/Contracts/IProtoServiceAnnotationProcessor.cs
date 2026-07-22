using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoServiceAnnotationProcessor : IProtoAnnotationProcessor<ProtoService>
{
    void Process(ProtoService src);
}