using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.ProtoTypeClassifiers;

internal sealed class EnumTypeClassifier : IProtoTypeClassifier
{
    public int Order => 2;

    public bool TryClassify(
        ProtoProperty property,
        out ProtoTypeKind kind)
    {
        if (!property.IsEnum)
        {
            kind = default;
            return false;
        }

        kind = ProtoTypeKind.Enum;
        return true;
    }
}