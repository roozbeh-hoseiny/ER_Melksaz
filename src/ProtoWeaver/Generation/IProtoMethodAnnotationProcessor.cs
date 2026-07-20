using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

public interface IProtoMethodAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoMethod src);
}
public interface IProtoMethodGenerationStep : IGenerationStep
{
    void Execute(ProtoMethod src, CSharpClassBuilder builder);
}