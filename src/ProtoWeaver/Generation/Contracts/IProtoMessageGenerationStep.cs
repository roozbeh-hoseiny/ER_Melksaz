using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoMessageGenerationStep : IGenerationStep
{
    void Execute(ProtoMessage src, GenerationContext context);
}
