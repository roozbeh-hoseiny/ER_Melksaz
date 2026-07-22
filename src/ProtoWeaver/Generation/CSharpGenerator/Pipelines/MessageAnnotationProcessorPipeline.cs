using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.Pipelines;

internal sealed class MessageAnnotationProcessorPipeline
{
    private readonly IReadOnlyList<IProtoMessageAnnotationProcessor> _steps;
    private readonly PropertyAnnotationProcessorPipeline _propertyAnnotationProcessorPipeline;

    public MessageAnnotationProcessorPipeline(
        PropertyAnnotationProcessorPipeline propertyAnnotationProcessorPipeline,
        IEnumerable<IProtoMessageAnnotationProcessor> steps)
    {
        this._steps = steps.OrderBy(x => x.Order).ToList();
        this._propertyAnnotationProcessorPipeline = propertyAnnotationProcessorPipeline;
    }

    public void Execute(ProtoMessage message)
    {
        foreach (var property in message.Properties)
        {
            this._propertyAnnotationProcessorPipeline.Execute(property, message);
        }

        foreach (var step in this._steps)
        {
            step.Process(message);
        }
    }
}
