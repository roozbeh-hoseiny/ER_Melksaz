using Google.Protobuf.Reflection;
using ProtoWeaver.Models;

namespace ProtoWeaver;

public static class CSharpPropertyResolver
{
    public static CSharpProperty Resolve(ProtoProperty property)
    {
        ArgumentNullException.ThrowIfNull(property);

        var type = ResolveType(property);

        return new CSharpProperty
        {
            Name = property.Name,
            Type = type,
            IsNullable = ResolveNullable(property),
            DefaultValue = ResolveDefaultValue(property, type),
        };
    }
    private static bool ResolveNullable(ProtoProperty property)
    {
        if (property.IsRepeated)
            return false;

        if (property.IsMessage)
            return true;

        return property.IsNullable;
    }
    private static CSharpType ResolveType(ProtoProperty property)
    {
        var elementType = ResolveElementType(property);

        if (!property.IsRepeated)
            return elementType;

        return new CSharpType
        {
            Name = $"List<{elementType.Name}>",
            IsCollection = true,
            IsValueType = false
        };
    }
    private static CSharpType ResolveElementType(ProtoProperty property)
    {
        if (property.IsMessage)
        {
            if (property.Message is not null &&
                property.Message.Package.Equals("google.protobuf") &&
                property.Message.Properties is not null &&
                property.Message.Properties.Count == 1)
            {
                return CreateCsharpTypeFromProtoFieldType(
                    property.Message.Properties[0]!.FieldType);
            }
            return new CSharpType
            {
                Name = property.Message!.Name,
                IsValueType = false
            };
        }

        if (property.IsEnum)
        {
            return new CSharpType
            {
                Name = property.EnumName!,
                IsValueType = true
            };
        }

        return CreateCsharpTypeFromProtoFieldType(property.FieldType);
    }

    private static CSharpType CreateCsharpTypeFromProtoFieldType(FieldType fieldType) => fieldType switch
    {
        FieldType.String => new CSharpType
        {
            Name = "string",
            IsValueType = false
        },

        FieldType.Bool => new CSharpType
        {
            Name = "bool",
            IsValueType = true
        },

        FieldType.Int32 => new CSharpType
        {
            Name = "int",
            IsValueType = true
        },

        FieldType.Int64 => new CSharpType
        {
            Name = "long",
            IsValueType = true
        },

        FieldType.UInt32 => new CSharpType
        {
            Name = "uint",
            IsValueType = true
        },

        FieldType.UInt64 => new CSharpType
        {
            Name = "ulong",
            IsValueType = true
        },

        FieldType.Float => new CSharpType
        {
            Name = "float",
            IsValueType = true
        },

        FieldType.Double => new CSharpType
        {
            Name = "double",
            IsValueType = true
        },

        FieldType.Bytes => new CSharpType
        {
            Name = "byte[]",
            IsValueType = false
        },

        _ => throw new NotSupportedException(
            $"FieldType '{fieldType}' is not supported.")
    };

    private static string ResolveDefaultValue(
        ProtoProperty property,
        CSharpType type)
    {
        if (property.IsRepeated)
            return "[]";

        if (property.IsMessage)
            return "null!";

        if (property.IsEnum)
            return "default";

        return type.Name switch
        {
            "string" => "string.Empty",
            "byte[]" => "[]",
            _ when type.IsValueType => "default",
            _ => "null!"
        };
    }
}