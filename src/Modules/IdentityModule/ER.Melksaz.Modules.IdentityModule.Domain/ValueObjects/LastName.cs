using ER.Melksaz.PrimitiveResults;
using System.Text.RegularExpressions;
namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct LastName
{
    public const int MaxLength = 50;
    public const int MinLength = 2;

    public static readonly LastName Empty = new(string.Empty);

    public string Value { get; } = string.Empty;

    private LastName(string value)
    {
        this.Value = value;
    }

    public static class ErrorCodes
    {
        public const string Empty = "FIRST_NAME_EMPTY";
        public const string TooLong = "FIRST_NAME_TOO_LONG";
        public const string TooShort = "FIRST_NAME_TOO_SHORT";
        public const string InvalidCharacters = "FIRST_NAME_INVALID_CHARACTERS";
    }

    [GeneratedRegex(
        @"^[آ-ی]+(?: [آ-ی]+)*$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex LastNameRegex();

    [GeneratedRegex(@"\s{2,}", RegexOptions.Compiled)]
    private static partial Regex MultiSpaceRegex();

    public static PrimitiveResult<LastName> Create(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            return PrimitiveResult.Failure<LastName>(validationResult);

        return new LastName(value);
    }

    public static LastName CreateUnsafe(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            throw new ArgumentException(validationResult.Error.Message);

        return new LastName(value);
    }

    public static PrimitiveResult IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure(
                ErrorCodes.Empty,
                "نام نمی‌تواند خالی باشد");

        if (value.Length < MinLength)
            return PrimitiveResult.Failure(
                ErrorCodes.TooShort,
                $"نام باید حداقل {MinLength} کاراکتر باشد");

        if (value.Length > MaxLength)
            return PrimitiveResult.Failure(
                ErrorCodes.TooLong,
                $"نام نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد");

        if (!LastNameRegex().IsMatch(value))
            return PrimitiveResult.Failure(
                ErrorCodes.InvalidCharacters,
                "نام فقط می‌تواند شامل حروف فارسی و فاصله باشد");

        return PrimitiveResult.Success();
    }

    private static string Normalize(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value
            .Replace('ي', 'ی')
            .Replace('ك', 'ک');

        value = MultiSpaceRegex().Replace(value, " ");

        return value;
    }

    public override string ToString() => this.Value;

    public static bool TryParse(string val, out LastName result)
    {
        result = LastName.Empty;
        try
        {
            result = CreateUnsafe(val);
            return true;
        }
        catch { }
        return false;
    }
}