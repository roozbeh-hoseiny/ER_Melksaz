using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Pipelines;

internal sealed class PropertyGenerationPipeline
{
    private readonly IReadOnlyList<IProtoPropertyGenerationStep> _steps;

    public PropertyGenerationPipeline(IEnumerable<IProtoPropertyGenerationStep> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }
    public void Execute(ProtoProperty service, GenerationContext context)
    {
        foreach (var step in this._steps)
        {
            step.Execute(service, context);
        }
    }
}