namespace ProtoWeaver.Generation.Contracts;

public interface IAnnotationProcessor
{
    int Order { get; }
}
public interface IProtoAnnotationProcessor<in T> : IAnnotationProcessor
{

}
