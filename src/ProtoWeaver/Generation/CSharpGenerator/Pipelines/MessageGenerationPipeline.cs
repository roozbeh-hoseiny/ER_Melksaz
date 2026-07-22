using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Pipelines;

internal sealed class MessageGenerationPipeline
{
    private readonly IReadOnlyList<IProtoMessageGenerationStep> _steps;

    public MessageGenerationPipeline(IEnumerable<IProtoMessageGenerationStep> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoMessage message, GenerationContext context)
    {
        foreach (var step in this._steps)
        {
            step.Execute(message, context);
        }
    }
}
