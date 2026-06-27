using Dapper;

namespace ER.Melksaz.BuildingBlocks.Persistence.DapperAccess.DapperSpecification;

public static class DapperSpecHelper
{
    public static IDapperSqlSpecification Equal<TValue>(
        string field,
        TValue value)
    {
        string paramName = $"_field_{field}_";

        var parameters = new DynamicParameters();
        parameters.Add(paramName, value);

        return new DapperSqlSpecification($"{QuoteName(field)} = @{paramName}", parameters);
    }

    public static IDapperSqlSpecification Like(
        string field,
        string value)
    {
        string paramName = $"_field_{field}_";

        var parameters = new DynamicParameters();
        parameters.Add(paramName, value);

        return new DapperSqlSpecification($"{QuoteName(field)} LIKE @{paramName}", parameters);
    }

    public static IDapperSqlSpecification Between<TValue>(
        string field,
        TValue start,
        TValue end)
    {
        string startParam = $"_field_{field}_start_";
        string endParam = $"_field_{field}_end_";

        var parameters = new DynamicParameters();
        parameters.Add(startParam, start);
        parameters.Add(endParam, end);

        return new DapperSqlSpecification($"{QuoteName(field)} BETWEEN @{startParam} AND @{endParam}", parameters);
    }

    public static string QuoteName(string name)
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
