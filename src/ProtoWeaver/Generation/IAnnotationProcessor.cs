namespace ProtoWeaver.Generation;

public interface IAnnotationProcessor
{
    int Order { get; }
}

public interface IGenerationStep
{
    int Order { get; }
}