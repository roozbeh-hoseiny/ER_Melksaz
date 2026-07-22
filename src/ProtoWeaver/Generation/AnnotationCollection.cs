using ProtoWeaver.Generation.Contracts;

namespace ProtoWeaver.Generation;

public sealed class AnnotationCollection
{
    private readonly Dictionary<Type, List<IProtoAnnotation>> _annotations = [];


    public void Add<TAnnotation>(TAnnotation annotation) where TAnnotation : IProtoAnnotation
    {
        ArgumentNullException.ThrowIfNull(annotation);

        var type = typeof(TAnnotation);

        if (!this._annotations.TryGetValue(type, out var list))
        {
            list = [];
            this._annotations[type] = list;
        }

        list.Add(annotation);
    }


    public IReadOnlyCollection<TAnnotation> GetAll<TAnnotation>() where TAnnotation : IProtoAnnotation
    {
        var type = typeof(TAnnotation);

        if (!this._annotations.TryGetValue(type, out var list))
            return [];

        return list
            .Cast<TAnnotation>()
            .ToArray();
    }

    public IReadOnlyCollection<TAnnotation> GetAllDerived<TAnnotation>() where TAnnotation : IProtoAnnotation
    {
        return this._annotations.Values
            .SelectMany(x => x)
            .OfType<TAnnotation>()
            .ToArray();
    }
    public TAnnotation? GetDerived<TAnnotation>() where TAnnotation : IProtoAnnotation
    {
        return this._annotations.Values
            .SelectMany(x => x)
            .OfType<TAnnotation>()
            .FirstOrDefault();
    }
    public TAnnotation? Get<TAnnotation>() where TAnnotation : IProtoAnnotation
    {
        var type = typeof(TAnnotation);

        if (!this._annotations.TryGetValue(type, out var list))
            return default;

        return list
            .OfType<TAnnotation>()
            .FirstOrDefault();
    }


    public bool Has<TAnnotation>() where TAnnotation : IProtoAnnotation
    {
        return this._annotations.ContainsKey(typeof(TAnnotation));
    }


    public bool Remove<TAnnotation>(TAnnotation annotation) where TAnnotation : IProtoAnnotation
    {
        var type = typeof(TAnnotation);

        if (!this._annotations.TryGetValue(type, out var list))
            return false;

        var result = list.Remove(annotation);

        if (list.Count == 0)
            this._annotations.Remove(type);

        return result;
    }

    public void Clear()
    {
        this._annotations.Clear();
    }
}