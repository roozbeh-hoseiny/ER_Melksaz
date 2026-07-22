namespace ProtoWeaver.Generation.Mapping;

public interface IAssignmentGeneratorResolver
{
    IAssignmentValueGenerator Resolve(AssignmentGenerationContext context);
}