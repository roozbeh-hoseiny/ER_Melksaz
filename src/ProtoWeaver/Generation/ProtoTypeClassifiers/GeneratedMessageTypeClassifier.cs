using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.ProtoTypeClassifiers;

internal sealed class GeneratedMessageTypeClassifier : IProtoTypeClassifier
{
    public int Order => 500;

    public bool TryClassify(ProtoProperty property, out ProtoTypeKind kind)
    {
        if (property.Message is null)
        {
            kind = default;
            return false;
        }

        kind = ProtoTypeKind.GeneratedMessage;
        return true;
    }
}