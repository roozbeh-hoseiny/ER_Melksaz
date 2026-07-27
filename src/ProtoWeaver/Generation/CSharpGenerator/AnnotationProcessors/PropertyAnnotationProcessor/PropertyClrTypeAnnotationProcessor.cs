using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.PropertyAnnotationProcessor;

internal sealed class PropertyClrTypeAnnotationProcessor : IProtoPropertyAnnotationProcessor
{
    private readonly IMessageNameResolver _messageNameResolver;

    public int Order => 5;

    public PropertyClrTypeAnnotationProcessor(IMessageNameResolver messageNameResolver)
    {
        this._messageNameResolver = messageNameResolver;
    }
    public void Process(ProtoProperty property, ProtoMessage message)
    {
        if (property.Name.Equals("ordinal", StringComparison.InvariantCultureIgnoreCase))
        {

        }
        string clrTypeNameSpace = "";
        var cSharpPropertyAnnotation = property.Annotations.Get<CSharpPropertyAnnotation>();

        if (cSharpPropertyAnnotation is null) return;

        var typeName = cSharpPropertyAnnotation.Type.Name;

        if (!cSharpPropertyAnnotation.Type.IsValueType && property.Message is not null)
        {
            var propMessage = this._messageNameResolver.GetOrCreate(property.Message);

            if (propMessage is not null)
            {
                clrTypeNameSpace = propMessage.Namespace;

                typeName = propMessage.ClassName;
            }
            else
            {
                typeName = CSharpPropertyResolver.Resolve(property).Type.Name;
            }
        }

        if (cSharpPropertyAnnotation.Type.IsCollection)
        {
            typeName = $"IReadOnlyList<{typeName}>";
        }

        if (cSharpPropertyAnnotation.IsNullable && cSharpPropertyAnnotation.Type.IsValueType)
        {
            typeName += "?";
        }
        else if (cSharpPropertyAnnotation.IsNullable)
        {
            typeName += "?";
        }

        property.AddAnnotation(new PropertyClrTypeAnnotation()
        {
            ClrType = typeName,
            ClrTypeNamespace = clrTypeNameSpace
        });
    }
}