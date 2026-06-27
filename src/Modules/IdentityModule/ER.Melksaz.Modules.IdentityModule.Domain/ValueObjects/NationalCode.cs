using ER.Melksaz.PrimitiveResults;
using System.Text.RegularExpressions;

namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct NationalCode
{
    public static readonly NationalCode Empty = new(string.Empty);

    [GeneratedRegex(@"^\d{10}$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline)]
    private static partial Regex NationalCodeFormatRegex();


    public string Value { get; } = string.Empty;

    private NationalCode(string value)
    {
        this.Value = value;
    }

    public static PrimitiveResult<NationalCode> Create(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value)) return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure) return PrimitiveResult.Failure<NationalCode>(validationResult);

        var result = new NationalCode(value);

        return result;
    }

    public static NationalCode CreateUnsafe(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value)) return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure) throw new ArgumentException("کد ملی اشتباه است");

        var result = new NationalCode(value);

        return result;
    }

    public static PrimitiveResult IsValid(string value)
    {
        if (!NationalCodeFormatRegex().IsMatch(value)) return PrimitiveResult.Failure("", "کد ملی اشتباه است");

        var check = Convert.ToInt32(value.Substring(9, 1));
        var sum = Enumerable.Range(0, 9)
            .Select(x => Convert.ToInt32(value.Substring(x, 1)) * (10 - x))
            .Sum() % 11;
        if (
            (sum < 2 && check == sum) ||
            (sum >= 2 && check + sum == 11)) return PrimitiveResult.Success();

        return PrimitiveResult.Failure("", "کد ملی اشتباه است");
    }

    public static NationalCode GenerateRandom()
    {
        string firstNine = string.Concat(Enumerable.Range(0, 9)
               .Select(_ => Random.Shared.Next(0, 10).ToString()));

        int sum = Enumerable.Range(0, 9)
               .Select(i => (firstNine[i] - '0') * (10 - i))
               .Sum() % 11;

        int checkDigit = sum < 2 ? sum : 11 - sum;

        string result = $"{firstNine}{checkDigit}";

        return CreateUnsafe(result);
    }

    public override string ToString() => this.Value ?? string.Empty;

    public static bool TryParse(string val, out NationalCode result)
    {
        result = NationalCode.Empty;
        try
        {
            result = CreateUnsafe(val);
            return true;
        }
        catch { }
        return false;
    }
}
