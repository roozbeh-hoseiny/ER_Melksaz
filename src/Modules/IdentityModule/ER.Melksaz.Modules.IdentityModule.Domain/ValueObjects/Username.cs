using ER.Melksaz.PrimitiveResults;
using System.Text.RegularExpressions;

namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct Username
{
    public const int MaxLength = 30;
    public const int MinLength = 3;

    public static readonly Username Empty = new(string.Empty);

    public string Value { get; } = string.Empty;

    private Username(string value)
    {
        this.Value = value;
    }

    public static class ErrorCodes
    {
        public const string Empty = "USERNAME_EMPTY";
        public const string TooLong = "USERNAME_TOO_LONG";
        public const string TooShort = "USERNAME_TOO_SHORT";
        public const string InvalidCharacters = "USERNAME_INVALID_CHARACTERS";
    }

    // Allowed: a-z A-Z 0-9 _ .
    [GeneratedRegex(@"^[a-zA-Z0-9._]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex UsernameRegex();

    public static PrimitiveResult<Username> Create(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            return PrimitiveResult.Failure<Username>(validationResult);

        return new Username(value);
    }

    public static Username CreateUnsafe(string value)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validationResult = IsValid(value);

        if (validationResult.IsFailure)
            throw new ArgumentException(validationResult.Error.Message);

        return new Username(value);
    }

    public static PrimitiveResult IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure(
                ErrorCodes.Empty,
                "نام کاربری نمی‌تواند خالی باشد");

        if (value.Length < MinLength)
            return PrimitiveResult.Failure(
                ErrorCodes.TooShort,
                $"نام کاربری باید حداقل {MinLength} کاراکتر باشد");

        if (value.Length > MaxLength)
            return PrimitiveResult.Failure(
                ErrorCodes.TooLong,
                $"نام کاربری نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد");

        if (!UsernameRegex().IsMatch(value))
            return PrimitiveResult.Failure(
                ErrorCodes.InvalidCharacters,
                "نام کاربری فقط می‌تواند شامل حروف انگلیسی، عدد، نقطه یا زیرخط (_) باشد");

        return PrimitiveResult.Success();
    }

    private static string Normalize(string value)
    {
        value = value?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        // Convert Persian/Arabic chars to English equivalents if needed
        value = value
            .Replace('ي', 'y')
            .Replace('ك', 'k');

        return value;
    }

    public override string ToString() => this.Value ?? string.Empty;

    public static bool TryParse(string val, out Username result)
    {
        result = Username.Empty;
        try
        {
            result = CreateUnsafe(val);
            return true;
        }
        catch { }
        return false;
    }
}
