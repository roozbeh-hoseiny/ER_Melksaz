using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMethodGenerationStep : IGenerationStep
{
    void Execute(ProtoMethod src, GenerationContext context);
}