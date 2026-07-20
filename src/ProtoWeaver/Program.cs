using ProtoWeaver;
using ProtoWeaver.Generation;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
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

foreach (var (_, message) in protoModel.Messages)
{
    foreach (var annotationProcessor in protoMessageAnnotationProcessors)
    {
        annotationProcessor.Process(message);
    }
}

var googleMessages = protoModel.Messages
    .Select(x => x.Value)
    .Where(message => message.Annotations.Has<GoogleMessageType>())
    .ToArray();


using (System.IO.StreamWriter writer = new StreamWriter("./2.json", false, System.Text.Encoding.UTF8))
{
    writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(protoModel, new Newtonsoft.Json.JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    }));
    writer.Close();
}
var x = 0;
//var generator = new CSharpClassGenerator("MyNameSpace");

//var dir = System.IO.Path.Combine(
//            Environment.CurrentDirectory,
//            "generatedFiles");
//System.IO.Directory.CreateDirectory(dir);


//foreach (var generatedFile in generator.Generate(protoModel))
//{
//    System.IO.File.WriteAllText(
//        System.IO.Path.Combine(dir, generatedFile.FileName),
//        generatedFile.Content);
//}


