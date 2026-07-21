using ProtoWeaver.Generation.CSharpGenerator;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class ProtoWeaverGenerator
{
    private readonly MessageAnnotationProcessorPipeline _messageAnnotationProcessorPipeline;
    private readonly ServiceAnnotationProcessorPipeline _serviceAnnotationProcessorPipeline;
    private readonly MessageGenerationPipeline _messageGenerationPipeline;
    private readonly ServiceGenerationPipeline _serviceGenerationPipeline;
    private readonly ICSharpDocumentWriter _writer;

    public ProtoWeaverGenerator(
        ServiceAnnotationProcessorPipeline serviceAnnotationProcessorPipeline,
        MessageAnnotationProcessorPipeline messageAnnotationProcessorPipeline,
        MessageGenerationPipeline messageGenerationPipeline,
        ServiceGenerationPipeline serviceGenerationPipeline,
        ICSharpDocumentWriter writer)
    {
        this._serviceAnnotationProcessorPipeline = serviceAnnotationProcessorPipeline;
        this._messageAnnotationProcessorPipeline = messageAnnotationProcessorPipeline;
        this._messageGenerationPipeline = messageGenerationPipeline;
        this._serviceGenerationPipeline = serviceGenerationPipeline;
        this._writer = writer;
    }

    public void Generate(ProtoModel model, string outputDirectory)
    {
        var context = new GenerationContext();

        foreach (var message in model.Messages.Values)
        {
            this._messageAnnotationProcessorPipeline.Execute(message);
        }

        foreach (var service in model.Services)
        {
            this._serviceAnnotationProcessorPipeline.Execute(service);
        }

        foreach (var message in model.Messages.Values)
        {
            this._messageGenerationPipeline.Execute(message, context);
        }

        foreach (var service in model.Services)
        {
            this._serviceGenerationPipeline.Execute(service, context);
        }

        this._writer.Write(context, outputDirectory);
    }
}
