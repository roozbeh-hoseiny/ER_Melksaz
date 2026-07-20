using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

public interface IProtoPropertyAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoProperty src);
}
public interface IProtoPropertyGenerationStep : IGenerationStep
{
    void Execute(ProtoProperty src, CSharpClassBuilder builder);
}