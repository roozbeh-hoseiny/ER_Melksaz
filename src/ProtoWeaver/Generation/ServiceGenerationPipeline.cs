using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class ServiceGenerationPipeline
{
    private readonly IReadOnlyList<IProtoServiceGenerationStep> _steps;

    public ServiceGenerationPipeline(IEnumerable<IProtoServiceGenerationStep> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoService service, GenerationContext context)
    {
        foreach (var step in this._steps)
        {
            step.Execute(service, context);
        }
    }
}