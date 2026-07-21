using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class MessageGenerationPipeline
{
    private readonly IReadOnlyList<IProtoMessageGenerationStep> _steps;

    public MessageGenerationPipeline(IEnumerable<IProtoMessageGenerationStep> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoMessage service, GenerationContext context)
    {
        foreach (var step in this._steps)
        {
            step.Execute(service, context);
        }
    }
}
