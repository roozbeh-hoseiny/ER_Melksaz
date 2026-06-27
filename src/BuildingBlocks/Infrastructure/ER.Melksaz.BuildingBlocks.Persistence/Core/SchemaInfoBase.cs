namespace ER.Melksaz.BuildingBlocks.Persistence.Core;

public class SchemaInfoBase
{
    public string Outbox_TableName = "OutboxMessages";
    public string Name { get; init; } = string.Empty;

    public SchemaInfoBase(string name) => this.Name = name;

    public string GetFullTableName(string tableName) => $"{QuoteName(this.GetSchemaName())}.{QuoteName(tableName)}";

    private string GetSchemaName() => string.IsNullOrWhiteSpace(this.Name)
         ? "dbo"
        : this.Name.Trim();

    private static string QuoteName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        ReadOnlySpan<char> span = name.AsSpan();

        bool hasBracket = span.Contains('[') || span.Contains(']');

        if (!hasBracket)
        {
            return $"[{name}]";
        }

        // Escape closing brackets
        Span<char> buffer = stackalloc char[span.Length * 2];
        int pos = 0;

        foreach (char c in span)
        {
            buffer[pos++] = c;

            if (c == ']')
            {
                buffer[pos++] = ']';
            }
        }

        return $"[{new string(buffer.Slice(0, pos))}]";
    }
}
