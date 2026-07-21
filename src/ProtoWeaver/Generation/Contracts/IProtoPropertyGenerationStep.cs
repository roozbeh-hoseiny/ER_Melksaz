using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoPropertyGenerationStep : IGenerationStep
{
    void Execute(ProtoProperty src, GenerationContext context);
}