using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Pipelines;

internal sealed class MessageGenerationPipeline
{
    private readonly IReadOnlyList<IProtoMessageGenerationStep> _steps;
    private readonly PropertyGenerationPipeline _propertyGenerationPipeline;

    public MessageGenerationPipeline(
        PropertyGenerationPipeline propertyGenerationPipeline,
        IEnumerable<IProtoMessageGenerationStep> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
        this._propertyGenerationPipeline = propertyGenerationPipeline;
        this._steps = steps.OrderBy(x => x.Order).ToList();
    }

    public void Execute(ProtoMessage message, GenerationContext context)
    {
        foreach (var property in message.Properties)
        {
            this._propertyGenerationPipeline.Execute(property, context);
        }
        foreach (var step in this._steps)
        {
            step.Execute(message, context);
        }
    }
}
