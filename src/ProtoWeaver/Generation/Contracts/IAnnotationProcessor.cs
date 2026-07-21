namespace ProtoWeaver.Generation.Contracts;

public interface IAnnotationProcessor
{
    int Order { get; }
}

public interface IGenerationStep
{
    int Order { get; }
}