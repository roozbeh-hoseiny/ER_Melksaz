using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.PropertyAnnotationProcessor;

internal sealed class RestrictionTypeAnnotationProcessor : IProtoPropertyAnnotationProcessor
{
    public int Order => 2;

    public void Process(ProtoProperty property, ProtoMessage message)
    {
        if (property.Name.Equals("ResourceId"))
        {
            property.AddAnnotation(ResourceIdPropertyAnnotation.Instance);
        }
        if (property.Name.Equals("MyId"))
        {
            property.AddAnnotation(MyIdPropertyAnnotation.Instance);
        }
        if (property.Name.Equals("RestrictedId"))
        {
            property.AddAnnotation(RestrictedIdPropertyAnnotation.Instance);
        }
    }
}
