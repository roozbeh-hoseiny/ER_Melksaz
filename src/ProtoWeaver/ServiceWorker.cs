using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoWeaver.Generation;

namespace ProtoWeaver;

internal sealed class ServiceWorker : BackgroundService
{
    private readonly ProtoWeaverGenerator _generator;
    private readonly ILogger<ServiceWorker> _logger;

    public ServiceWorker(
        ProtoWeaverGenerator generator,
        ILogger<ServiceWorker> logger)
    {
        this._generator = generator;
        this._logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var assemblyPath = @"C:\works\PersonalWorks\Azure_Sanjesh\Sanjesh\src\Services\SchoolServices\ER.Sanjesh.School.ProtoContract\bin\Debug\netstandard2.1\ER.Sanjesh.School.ProtoContract.dll";

        var loader = new AssemblyLoader();

        var assembly = loader.Load(assemblyPath);

        var rootDescriptors = AssemblyDescriptorScanner.Scan(assembly);

        var walker = new FileDescriptorDependencyWalker();

        var descriptors = walker.Collect(rootDescriptors);

        ProtoWeaver.Models.ProtoModel? protoModel = DescriptorReader.Read(descriptors?.ToArray() ?? []);

        this._generator.Generate(protoModel, @"C:\works\PersonalWorks\ER_Melksaz\src\ProtoWeaver\_generated");

        this._logger.LogInformation(protoModel.Messages.Count.ToString());
    }
}
