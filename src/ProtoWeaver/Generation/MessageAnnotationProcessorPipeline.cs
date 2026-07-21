using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class MessageAnnotationProcessorPipeline
{
    private readonly IReadOnlyList<IProtoMessageAnnotationProcessor> _steps;

    public MessageAnnotationProcessorPipeline(IEnumerable<IProtoMessageAnnotationProcessor> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoMessage service)
    {
        foreach (var step in this._steps)
        {
            step.Process(service);
        }
    }
}