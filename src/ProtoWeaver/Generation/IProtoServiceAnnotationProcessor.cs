using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

public interface IProtoServiceAnnotationProcessor : IAnnotationProcessor
{
    void Process(ProtoService src);
}
public interface IProtoServiceGenerationStep : IGenerationStep
{
    void Execute(ProtoService src, CSharpClassBuilder builder);
}