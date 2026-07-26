using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation;

internal sealed class ProtoTypeClassifierResolver : IProtoTypeClassifierResolver
{
    private readonly IReadOnlyList<IProtoTypeClassifier> _classifiers;

    public ProtoTypeClassifierResolver(IEnumerable<IProtoTypeClassifier> classifiers)
    {
        this._classifiers = classifiers
            .OrderBy(x => x.Order)
            .ToList();
    }

    public ProtoTypeKind Classify(ProtoProperty property)
    {
        foreach (var classifier in this._classifiers)
        {
            if (classifier.TryClassify(
                    property,
                    out var kind))
            {
                return kind;
            }
        }

        throw new InvalidOperationException($"No ProtoTypeClassifier found for property '{property.Name}'.");
    }
}