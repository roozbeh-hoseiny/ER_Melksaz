using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.ProtoTypeClassifiers;

internal sealed class DurationTypeClassifier : IProtoTypeClassifier
{
    public int Order => 30;

    public bool TryClassify(
        ProtoProperty property,
        out ProtoTypeKind kind)
    {
        if (property.Message is null)
        {
            kind = default;
            return false;
        }

        if (!string.Equals(
                property.Message.FileName,
                "google/protobuf/duration.proto",
                StringComparison.Ordinal))
        {
            kind = default;
            return false;
        }

        kind = ProtoTypeKind.Duration;
        return true;
    }
}