using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using ER.Melksaz.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoWeaver;
using ProtoWeaver.Builder;
using ProtoWeaver.Generation.CSharpGenerator;
using ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;
using Serilog;
try
{
    var host = CreateHostBuilder(args).Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
}
IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, logger) =>
        {
            logger.ConfigureAppLogging(context.Configuration, services);
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            AppConfigurationHelper.ConfigureHostedAppConfiguration(hostingContext, config, "Database:SampleAppConfig", SettingVersion.Version1);
        })
        .ConfigureServices((hostBuilderContext, services) =>
        {
            _ = HostedServiceServiceInstallerHelper.InstallHostedServiceServices(
                services,
                hostBuilderContext.Configuration,
                hostBuilderContext.HostingEnvironment,
                ProtoWeaverAssemblyReference.Assembly);

            services.AddProtoWeaver(builder =>
            {
                builder.WithWriter<CSharpDocumentWriter>();
                builder.ScanAssembly(ProtoWeaverAssemblyReference.Assembly);

                builder.Services.AddSingleton<IMessageNameResolver, MessageNameResolver>();
            });

            //services.AddProtoWeaver(builder => {

            //    builder.WithWriter<CSharpDocumentWriter>();

            //    builder.AddProtoMessageAnnotationProcessor<GoogleMessageTypeProcessor>();
            //    builder.AddProtoMessageAnnotationProcessor<SharedMessageTypeProcessor>();
            //    builder.AddProtoMessageAnnotationProcessor<ApiRequestMessageTypeProcessor>();
            //    builder.AddProtoMessageAnnotationProcessor<ApiResponseMessageTypeProcessor>();
            //    builder.AddProtoMessageAnnotationProcessor<ApiReplyMessageTypeProcessor>();

            //    builder.AddProtoServiceAnnotationProcessor<CSharpClassProcessor>();

            //    builder.AddProtoServiceGenerationStep<ClassDeclarationStep>();
            //});

            //services.AddSingleton<MessageGenerationPipeline>();
            //services.AddSingleton<ServiceGenerationPipeline>();
            //services.AddSingleton<ServiceAnnotationProcessorPipeline>();
            //services.AddSingleton<MessageAnnotationProcessorPipeline>();
            //services.AddSingleton<ProtoWeaverGenerator>();

            //services.AddSingleton<ICSharpDocumentWriter, CSharpDocumentWriter>();

            //services.AddSingleton<IProtoMessageAnnotationProcessor, GoogleMessageTypeProcessor>();
            //services.AddSingleton<IProtoMessageAnnotationProcessor, SharedMessageTypeProcessor>();
            //services.AddSingleton<IProtoMessageAnnotationProcessor, ApiRequestMessageTypeProcessor>();
            //services.AddSingleton<IProtoMessageAnnotationProcessor, ApiResponseMessageTypeProcessor>();
            //services.AddSingleton<IProtoMessageAnnotationProcessor, ApiReplyMessageTypeProcessor>();

            //services.AddSingleton<IProtoServiceAnnotationProcessor, CSharpClassProcessor>();
            //services.AddSingleton<IProtoServiceAnnotationProcessor, ServiceDocumentProcessor>();
            //services.AddSingleton<IProtoServiceAnnotationProcessor, ServiceNameProcessor>();

            //services.AddSingleton<IProtoServiceGenerationStep, ClassDeclarationStep>();

            //A2HMNO050B0000ED030696

            services.AddHostedService<ServiceWorker>();
        });




//List<IProtoMessageAnnotationProcessor> protoMessageAnnotationProcessors = new List<IProtoMessageAnnotationProcessor>()
//{
//    new GoogleMessageTypeProcessor(),
//    new SharedMessageTypeProcessor(),
//    new ApiRequestMessageTypeProcessor(),
//    new ApiResponseMessageTypeProcessor(),
//    new ApiReplyMessageTypeProcessor()
//}.OrderBy(x => x.Order).ToList();

//List<IProtoServiceAnnotationProcessor> protoServiceAnnotationProcessors = new List<IProtoServiceAnnotationProcessor>()
//{
//    new CSharpClassProcessor()
//}.OrderBy(x => x.Order).ToList();

//List<IProtoServiceGenerationStep> protoServiceGenerationSteps = new List<IProtoServiceGenerationStep>()
//{
//    new ClassDeclarationStep()
//}.OrderBy(x => x.Order).ToList();

//foreach (var (_, message) in protoModel.Messages)
//{
//    foreach (var annotationProcessor in protoMessageAnnotationProcessors)
//    {
//        annotationProcessor.Process(message);
//    }
//}
//foreach (var service in protoModel.Services)
//{
//    foreach (var annotationProcessor in protoServiceAnnotationProcessors)
//    {
//        annotationProcessor.Process(service);
//    }
//}

//using (System.IO.StreamWriter writer = new StreamWriter("./2.json", false, System.Text.Encoding.UTF8))
//{
//    writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(protoModel, new Newtonsoft.Json.JsonSerializerSettings()
//    {
//        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//    }));
//    writer.Close();
//}