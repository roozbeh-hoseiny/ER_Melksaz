using ProtoWeaver;
using ProtoWeaver.Generation;
using ProtoWeaver.Generation.CSharpGenerator.GenerationSteps;
using ProtoWeaver.Generation.CSharpGenerator.Processors;

var assemblyPath = @"C:\works\PersonalWorks\Azure_Sanjesh\Sanjesh\src\Services\SchoolServices\ER.Sanjesh.School.ProtoContract\bin\Debug\netstandard2.1\ER.Sanjesh.School.ProtoContract.dll";

var loader = new AssemblyLoader();

var assembly = loader.Load(assemblyPath);

var rootDescriptors = AssemblyDescriptorScanner.Scan(assembly);

var walker = new FileDescriptorDependencyWalker();

var descriptors = walker.Collect(rootDescriptors);

ProtoWeaver.Models.ProtoModel? protoModel = DescriptorReader.Read(descriptors?.ToArray() ?? []);

List<IProtoMessageAnnotationProcessor> protoMessageAnnotationProcessors = new List<IProtoMessageAnnotationProcessor>()
{
    new GoogleMessageTypeProcessor(),
    new SharedMessageTypeProcessor(),
    new ApiRequestMessageTypeProcessor(),
    new ApiResponseMessageTypeProcessor(),
    new ApiReplyMessageTypeProcessor()
}.OrderBy(x => x.Order).ToList();

List<IProtoServiceAnnotationProcessor> protoServiceAnnotationProcessors = new List<IProtoServiceAnnotationProcessor>()
{
    new CSharpClassProcessor()
}.OrderBy(x => x.Order).ToList();

List<IProtoServiceGenerationStep> protoServiceGenerationSteps = new List<IProtoServiceGenerationStep>()
{
    new ClassDeclarationStep()
}.OrderBy(x => x.Order).ToList();

foreach (var (_, message) in protoModel.Messages)
{
    foreach (var annotationProcessor in protoMessageAnnotationProcessors)
    {
        annotationProcessor.Process(message);
    }
}
foreach (var service in protoModel.Services)
{
    foreach (var annotationProcessor in protoServiceAnnotationProcessors)
    {
        annotationProcessor.Process(service);
    }
}
foreach (var service in protoModel.Services)
{
    CSharpClassBuilder builder = new CSharpClassBuilder();
    foreach (var step in protoServiceGenerationSteps)
    {
        step.Execute(service, builder);
    }
    using (System.IO.StreamWriter writer = new StreamWriter($"./{builder.ClassName}.cs", false, System.Text.Encoding.UTF8))
    {
        writer.WriteLine(builder.Build());
        writer.Close();
    }
}

//using (System.IO.StreamWriter writer = new StreamWriter("./2.json", false, System.Text.Encoding.UTF8))
//{
//    writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(protoModel, new Newtonsoft.Json.JsonSerializerSettings()
//    {
//        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//    }));
//    writer.Close();
//}