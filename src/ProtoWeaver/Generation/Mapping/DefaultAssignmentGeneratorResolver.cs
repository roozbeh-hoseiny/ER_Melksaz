namespace ProtoWeaver.Generation.Mapping;

internal sealed class DefaultAssignmentGeneratorResolver : IAssignmentGeneratorResolver
{
    private readonly IReadOnlyList<IAssignmentValueGenerator> _generators;

    public DefaultAssignmentGeneratorResolver(IEnumerable<IAssignmentValueGenerator> generators)
    {
        this._generators = generators.ToList();
    }

    public IAssignmentValueGenerator Resolve(AssignmentGenerationContext context)
    {
        foreach (var generator in this._generators ?? [])
        {
            if (generator.CanHandle(context))
                return generator;
        }

        throw new InvalidOperationException($"No assignment generator found for property '{context.Property.Name}'.");
    }
}