using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation;

internal sealed class DefaultAssignmentGeneratorResolver : IAssignmentGeneratorResolver
{
    private readonly IReadOnlyDictionary<ProtoTypeKind, IAssignmentValueGenerator> _generators;

    public DefaultAssignmentGeneratorResolver(IEnumerable<IAssignmentValueGenerator> generators)
    {
        this._generators = generators.ToDictionary(x => x.Kind);
    }

    public IAssignmentValueGenerator Resolve(ProtoTypeKind kind)
    {
        if (this._generators.TryGetValue(kind, out var generator))
        {
            return generator;
        }

        throw new InvalidOperationException($"No assignment generator registered for '{kind}'.");
    }
}