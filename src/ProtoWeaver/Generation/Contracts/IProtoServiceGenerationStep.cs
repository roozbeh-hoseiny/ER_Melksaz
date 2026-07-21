using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoServiceGenerationStep : IGenerationStep
{
    void Execute(ProtoService src, GenerationContext context);
}