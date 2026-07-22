namespace ProtoWeaver.Generation.Contracts;

public interface IGenerationStep
{
    int Order { get; }
}
public interface IGenerationStep<in T> : IGenerationStep
{
    void Execute(T src, GenerationContext context);
}