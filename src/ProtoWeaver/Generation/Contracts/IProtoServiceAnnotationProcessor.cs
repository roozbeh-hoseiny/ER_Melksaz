using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoServiceAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoService src);
}
