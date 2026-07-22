using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Pipelines;

internal sealed class PropertyAnnotationProcessorPipeline
{
    private readonly IReadOnlyList<IProtoPropertyAnnotationProcessor> _steps;

    public PropertyAnnotationProcessorPipeline(IEnumerable<IProtoPropertyAnnotationProcessor> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoProperty property)
    {
        foreach (var step in this._steps)
        {
            step.Process(property);
        }
    }
}