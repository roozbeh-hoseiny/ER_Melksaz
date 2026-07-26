namespace ProtoWeaver.Generation.Contracts;

public interface IAssignmentGeneratorResolver
{
    IAssignmentValueGenerator Resolve(ProtoTypeKind kind);
}