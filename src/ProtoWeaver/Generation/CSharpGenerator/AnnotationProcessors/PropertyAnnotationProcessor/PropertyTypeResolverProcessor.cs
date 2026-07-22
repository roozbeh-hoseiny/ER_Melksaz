using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.PropertyAnnotationProcessor;

internal sealed class PropertyTypeResolverProcessor : IProtoPropertyAnnotationProcessor
{
    public int Order => 1;

    public void Process(ProtoProperty src)
    {
        var resolved = CSharpPropertyResolver.Resolve(src);
        var annotation = new CSharpPropertyAnnotation()
        {
            Name = resolved.Name,
            IsNullable = resolved.IsNullable,
            DefaultValue = resolved.DefaultValue,
            Type = new CSharpTypeAnnotation()
            {
                Name = resolved.Type.Name,
                IsCollection = resolved.Type.IsCollection,
                IsValueType = resolved.Type.IsValueType
            }
        };

        src.Annotations.Add(annotation);
    }

    public void Process(ProtoProperty property, ProtoMessage message)
    {
        var resolved = CSharpPropertyResolver.Resolve(property);
        var annotation = new CSharpPropertyAnnotation()
        {
            Name = resolved.Name,
            IsNullable = resolved.IsNullable,
            DefaultValue = resolved.DefaultValue,
            Type = new CSharpTypeAnnotation()
            {
                Name = resolved.Type.Name,
                IsCollection = resolved.Type.IsCollection,
                IsValueType = resolved.Type.IsValueType
            }
        };

        property.Annotations.Add(annotation);
    }
}