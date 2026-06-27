using ER.Melksaz.PrimitiveResults;
using System.Text.RegularExpressions;
namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct Email
{
    public static readonly Email Empty = new(string.Empty);

    public string Value { get; } = string.Empty;

    private Email(string value)
    {
        this.Value = value;
    }

    public enum ValidationMode
    {
        Basic,
        Rfc5322Strict
    }


    public static class ErrorCodes
    {
        public const string Empty = "EMAIL_EMPTY";
        public const string InvalidFormat = "EMAIL_INVALID_FORMAT";
        public const string InvalidRfc5322 = "EMAIL_INVALID_RFC5322";
        public const string InvalidDomain = "EMAIL_INVALID_DOMAIN";
        public const string GmailNormalizationFailed = "EMAIL_GMAIL_NORMALIZATION_FAILED";
    }


    [GeneratedRegex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex BasicRegex();

    // RFC 5322 simplified practical version (still not full spec monster)
    [GeneratedRegex(
        @"^(?:[a-zA-Z0-9_'^&/+-])+(?:\.(?:[a-zA-Z0-9_'^&/+-])+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex Rfc5322Regex();


    public static PrimitiveResult<Email> Create(
        string value,
        ValidationMode mode = ValidationMode.Basic,
        bool normalizeGmail = true)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure<Email>(ErrorCodes.Empty, "Email is empty");

        var validation = IsValid(value, mode, normalizeGmail);

        if (validation.IsFailure)
            return PrimitiveResult.Failure<Email>(validation);

        return new Email(value);
    }

    public static Email CreateUnsafe(
        string value,
        ValidationMode mode = ValidationMode.Basic,
        bool normalizeGmail = true)
    {
        value = Normalize(value);

        if (string.IsNullOrWhiteSpace(value))
            return Empty;

        var validation = IsValid(value, mode, normalizeGmail);

        if (validation.IsFailure)
            throw new ArgumentException(validation.Error.Message ?? "Invalid email");

        return new Email(value);
    }


    public static PrimitiveResult IsValid(
        string value,
        ValidationMode mode = ValidationMode.Basic,
        bool normalizeGmail = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure(ErrorCodes.Empty, "Email is empty");

        if (mode == ValidationMode.Basic)
        {
            if (!BasicRegex().IsMatch(value))
                return PrimitiveResult.Failure(ErrorCodes.InvalidFormat, "Invalid email format");
        }
        else
        {
            if (!Rfc5322Regex().IsMatch(value))
                return PrimitiveResult.Failure(ErrorCodes.InvalidRfc5322, "Invalid RFC5322 email format");
        }

        var atIndex = value.IndexOf('@');
        if (atIndex < 0)
            return PrimitiveResult.Failure(ErrorCodes.InvalidFormat, "Missing @");

        var domain = value[(atIndex + 1)..];

        if (string.IsNullOrWhiteSpace(domain))
            return PrimitiveResult.Failure(ErrorCodes.InvalidDomain, "Invalid domain");

        if (normalizeGmail)
        {
            var gmailCheck = ValidateGmailRules(value);
            if (gmailCheck.IsFailure)
                return gmailCheck;
        }

        return PrimitiveResult.Success();
    }


    private static string Normalize(string value)
    {
        value = value?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value;
    }

    private static PrimitiveResult ValidateGmailRules(string email)
    {
        var atIndex = email.IndexOf('@');
        if (atIndex < 0) return PrimitiveResult.Failure(ErrorCodes.InvalidFormat, "Invalid email");

        var local = email[..atIndex];
        var domain = email[(atIndex + 1)..];

        // Only apply Gmail rules
        if (domain is not ("gmail.com" or "googlemail.com"))
            return PrimitiveResult.Success();

        // Rule 1: remove dots
        local = local.Replace(".", string.Empty);

        // Rule 2: remove +tag
        var plusIndex = local.IndexOf('+');
        if (plusIndex >= 0)
            local = local[..plusIndex];

        if (string.IsNullOrWhiteSpace(local))
            return PrimitiveResult.Failure(
                ErrorCodes.GmailNormalizationFailed,
                "Invalid Gmail address after normalization");

        return PrimitiveResult.Success();
    }

    public override string ToString() => this.Value;

    public static bool TryParse(string val, out Email result)
    {
        result = Email.Empty;
        try
        {
            result = CreateUnsafe(val);
            return true;
        }
        catch { }
        return false;
    }
}