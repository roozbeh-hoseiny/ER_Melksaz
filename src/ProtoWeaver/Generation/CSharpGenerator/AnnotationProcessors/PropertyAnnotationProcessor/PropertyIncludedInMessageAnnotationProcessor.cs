using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.PropertyAnnotationProcessor;

internal sealed class PropertyIncludedInMessageAnnotationProcessor : IProtoPropertyAnnotationProcessor
{
    public int Order => 4;

    public void Process(ProtoProperty property, ProtoMessage message)
    {
        var restrictionAnnotation = property.Annotations.GetDerived<PropertyRestrictionTypeAnnotationBase>();

        if (restrictionAnnotation is MyIdPropertyAnnotation) return;

        property.AddAnnotation(PropertyIncludedInMessageAnnotation.Instance);
    }
}