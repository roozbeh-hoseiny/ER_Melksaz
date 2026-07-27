using Microsoft.Extensions.DependencyInjection;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Pipelines;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class ProtoWeaverGenerator
{
    private readonly MessageAnnotationProcessorPipeline _messageAnnotationProcessorPipeline;
    private readonly ServiceAnnotationProcessorPipeline _serviceAnnotationProcessorPipeline;
    private readonly MessageGenerationPipeline _messageGenerationPipeline;
    private readonly ServiceGenerationPipeline _serviceGenerationPipeline;
    private readonly IDocumentWriter _writer;
    private readonly IServiceProvider _serviceProvider;

    public ProtoWeaverGenerator(
        ServiceAnnotationProcessorPipeline serviceAnnotationProcessorPipeline,
        MessageAnnotationProcessorPipeline messageAnnotationProcessorPipeline,
        MessageGenerationPipeline messageGenerationPipeline,
        ServiceGenerationPipeline serviceGenerationPipeline,
        IDocumentWriter writer,
        IServiceProvider serviceProvider)
    {
        this._serviceAnnotationProcessorPipeline = serviceAnnotationProcessorPipeline;
        this._messageAnnotationProcessorPipeline = messageAnnotationProcessorPipeline;
        this._messageGenerationPipeline = messageGenerationPipeline;
        this._serviceGenerationPipeline = serviceGenerationPipeline;
        this._writer = writer;
        this._serviceProvider = serviceProvider;
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

    public IReadOnlyCollection<(string Processor, int Order)> GetAnnotationProcessorsOrder()
    {
        var annotationProcessors = this._serviceProvider.GetRequiredService<IEnumerable<IProtoMessageAnnotationProcessor>>();
        return annotationProcessors
            .Select(ap => (ap.GetType().Name, ap.Order))
            .OrderBy(x => x.Order)
            .ToArray();
    }
}
