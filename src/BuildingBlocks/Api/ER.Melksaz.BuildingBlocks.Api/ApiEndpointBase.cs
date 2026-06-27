namespace ER.Melksaz.BuildingBlocks.Api;

public abstract class ApiEndpointBase
{
    protected abstract ApiEndpointItem? Root { get; }

    protected string GetUrl(params string[]? segments)
    {
        var allSegments = this.GetSegments()
            .Concat(segments ?? [])
            .Where(static x => !string.IsNullOrWhiteSpace(x));

        return string.Join("/", allSegments);
    }

    protected EndpointInfo Create(
        string route,
        string description,
        string tag,
        string? name = null)
    {
        var url = this.GetUrl(route);

        return new EndpointInfo(
            Url: url,
            Name: name ?? url,
            Description: description,
            Tag: tag);
    }

    private IEnumerable<string> GetSegments()
    {
        var stack = new Stack<string>();

        var current = this.Root;

        while (current is not null)
        {
            if (!string.IsNullOrWhiteSpace(current.Segment))
            {
                stack.Push(current.Segment);
            }

            current = current.Parent;
        }

        return stack;
    }

    public override string ToString() => this.GetUrl();
}