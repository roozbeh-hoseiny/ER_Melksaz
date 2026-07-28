using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.ProtoTypeClassifiers;

internal sealed class TimestampTypeClassifier : IProtoTypeClassifier
{
    public int Order => 20;

    public bool TryClassify(ProtoProperty property, out ProtoTypeKind kind)
    {
        if (property.Message is null)
        {
            kind = default;
            return false;
        }

        if (!string.Equals(
                property.Message.FileName,
                "google/protobuf/timestamp.proto",
                StringComparison.Ordinal))
        {
            kind = default;
            return false;
        }

        kind = ProtoTypeKind.Timestamp;
        return true;
    }
}