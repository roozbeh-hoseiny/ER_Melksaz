using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation.CSharpGenerator.Annotations;

internal abstract class PropertyRestrictionTypeAnnotationBase : IProtoAnnotation;


internal sealed class RestrictedIdPropertyAnnotation : PropertyRestrictionTypeAnnotationBase
{
    public static readonly RestrictedIdPropertyAnnotation Instance = new();
}
internal sealed class MyIdPropertyAnnotation : PropertyRestrictionTypeAnnotationBase
{
    public static readonly MyIdPropertyAnnotation Instance = new();
}
internal sealed class ResourceIdPropertyAnnotation : PropertyRestrictionTypeAnnotationBase
{
    public static readonly ResourceIdPropertyAnnotation Instance = new();
}


