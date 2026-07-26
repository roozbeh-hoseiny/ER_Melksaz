using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.ProtoTypeClassifiers;

internal sealed class PrimitiveTypeClassifier : IProtoTypeClassifier
{
    public int Order => 1000;

    public bool TryClassify(
        ProtoProperty property,
        out ProtoTypeKind kind)
    {
        if (property.Message is not null)
        {
            kind = default;
            return false;
        }

        kind = ProtoTypeKind.Primitive;
        return true;
    }
}