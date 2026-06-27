using ER.Melksaz.PrimitiveResults;
using System.Text.RegularExpressions;
namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct Mobile
{
    public static readonly Mobile Empty = new(string.Empty);

    // Accepts:
    // 09123456789
    // +989123456789
    // 989123456789
    [GeneratedRegex(@"^(?:\+98|98|0)?9\d{9}$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline)]
    private static partial Regex IranianMobileRegex();

    public string Value { get; } = string.Empty;

    private Mobile(string value)
    {
        this.Value = value;
    }

    public static PrimitiveResult<Mobile> Create(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            return PrimitiveResult.Failure<Mobile>(validationResult);

        return new Mobile(value);
    }

    public static Mobile CreateUnsafe(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            throw new ArgumentException("شماره موبایل نامعتبر است");

        return new Mobile(value);
    }

    public static PrimitiveResult IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure("", "شماره موبایل نامعتبر است");

        if (!IranianMobileRegex().IsMatch(value))
            return PrimitiveResult.Failure("", "شماره موبایل نامعتبر است");

        return PrimitiveResult.Success();
    }

    private static string Normalize(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        // Convert +98 or 98 prefix to 0
        if (value.StartsWith("+98"))
            value = "0" + value[3..];
        else if (value.StartsWith("98") && value.Length == 12)
            value = "0" + value[2..];

        return value;
    }

    public static bool TryParse(string val, out Mobile result)
    {
        result = Mobile.Empty;
        try
        {
            result = CreateUnsafe(val);
            return true;
        }
        catch { }
        return false;
    }
}