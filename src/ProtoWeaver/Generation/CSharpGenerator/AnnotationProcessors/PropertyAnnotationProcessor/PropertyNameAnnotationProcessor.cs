using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.PropertyAnnotationProcessor;

internal sealed class PropertyNameAnnotationProcessor : IProtoPropertyAnnotationProcessor
{
    public int Order => 3;

    public void Process(ProtoProperty property, ProtoMessage message)
    {
        var name = property.Name;

        var restrictionAnnotation = property.Annotations.GetDerived<PropertyRestrictionTypeAnnotationBase>();

        if (restrictionAnnotation is RestrictedIdPropertyAnnotation)
        {
            name = "Id";
        }
        if (restrictionAnnotation is ResourceIdPropertyAnnotation)
        {
            name = "Id";
        }

        property.AddAnnotation(new PropertyNameAnnotation() { Name = name });
    }
}