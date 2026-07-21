using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator;

namespace ProtoWeaver.Generation;

public sealed class GenerationContext
{
    private readonly Dictionary<DocumentKey, CSharpDocument> _documents = [];

    public IReadOnlyCollection<CSharpDocument> Documents => this._documents.Values;

    public bool Contains(DocumentKey key) => this._documents.ContainsKey(key);

    public IEnumerable<CSharpDocument> Find(Func<CSharpDocument, bool> predicate)
    {
        return this._documents.Values.Where(predicate);
    }

    public CSharpDocument Get(DocumentKey key)
    {
        if (!this._documents.TryGetValue(key, out var document))
            throw new InvalidOperationException(
                $"Document '{key}' was not found.");

        return document;
    }

    public bool TryGet(DocumentKey key, out CSharpDocument? document)
    {
        return this._documents.TryGetValue(
            key,
            out document);
    }

    public void Add(CSharpDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        if (this._documents.ContainsKey(document.Key))
        {
            throw new InvalidOperationException(
                $"Document '{document.Key}' already exists.");
        }

        this._documents.Add(
            document.Key,
            document);
    }

    public TBuilder GetBuilder<TBuilder>(DocumentKey key)
        where TBuilder : class, ICSharpBuilder
    {
        var document = this.Get(key);

        return document.Builder as TBuilder
            ?? throw new InvalidOperationException(
                $"Document '{key}' is not using '{typeof(TBuilder).Name}'.");
    }

    public bool TryGetBuilder<TBuilder>(
        DocumentKey key,
        out TBuilder? builder)
        where TBuilder : class, ICSharpBuilder
    {
        builder = default;

        if (!this.TryGet(key, out var document))
            return false;

        if (document is null) return false;

        builder = document.Builder as TBuilder;

        return builder is not null;
    }

    public TBuilder GetOrCreate<TBuilder>(
        DocumentKey key,
        string fileName,
        Func<TBuilder> factory)
        where TBuilder : class, ICSharpBuilder
    {
        if (this._documents.TryGetValue(key, out var existing))
        {
            if (existing.Builder is TBuilder builder)
                return builder;

            throw new InvalidOperationException(
                $"Document '{key}' already exists with builder '{existing.Builder.GetType().Name}'.");
        }

        var newBuilder = factory();

        var document = new CSharpDocument
        {
            Key = key,
            FileName = fileName,
            Builder = newBuilder
        };

        this._documents.Add(
            key,
            document);

        return newBuilder;
    }

    public void AddAnnotation<T>(
        DocumentKey key,
        T annotation) where T : class, IProtoAnnotation
    {
        var document = this.Get(key);

        document!.Annotations.Add(annotation);
    }
}