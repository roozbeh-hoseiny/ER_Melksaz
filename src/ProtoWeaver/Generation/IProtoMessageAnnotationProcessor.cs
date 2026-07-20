using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

public interface IProtoMessageAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoMessage src);
}
public interface IProtoMessageGenerationStep : IGenerationStep
{
    void Execute(ProtoMessage src, CSharpClassBuilder builder);
}
