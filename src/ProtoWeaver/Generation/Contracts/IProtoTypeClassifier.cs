using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IProtoTypeClassifier
{
    int Order { get; }

    bool TryClassify(ProtoProperty property, out ProtoTypeKind kind);
}
