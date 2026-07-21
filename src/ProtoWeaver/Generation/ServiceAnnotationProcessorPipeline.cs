using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class ServiceAnnotationProcessorPipeline
{
    private readonly IReadOnlyList<IProtoServiceAnnotationProcessor> _steps;

    public ServiceAnnotationProcessorPipeline(IEnumerable<IProtoServiceAnnotationProcessor> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoService service)
    {
        foreach (var step in this._steps)
        {
            step.Process(service);
        }
    }
}
