using ER.Melksaz.BuildingBlocks.Helpers;
using ER.Melksaz.PrimitiveResults;
using System.Security.Cryptography;

namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly partial record struct PasswordHash
{
    private const int SaltSize = 16;     // 128-bit

    public const int MinLength = 8;
    public const int MaxLength = 128;

    public byte[] Hash { get; } = [];
    public byte[] Salt { get; } = [];

    private PasswordHash(byte[] hash, byte[] salt)
    {
        this.Hash = hash;
        this.Salt = salt;
    }

    public static class ErrorCodes
    {
        public const string Empty = "PASSWORD_EMPTY";
        public const string TooShort = "PASSWORD_TOO_SHORT";
        public const string TooLong = "PASSWORD_TOO_LONG";
        public const string Weak = "PASSWORD_WEAK";
        public const string InvalidHash = "PASSWORD_INVALID_HASH";
        public const string InvalidSalt = "PASSWORD_INVALID_SALT";
    }

    public static PrimitiveResult<PasswordHash> Create(string plainPassword)
    {
        plainPassword = Normalize(plainPassword);

        var validation = IsValid(plainPassword);
        if (validation.IsFailure)
            return PrimitiveResult.Failure<PasswordHash>(validation);

        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var hashBytes = HashPassword(plainPassword, saltBytes);

        return new PasswordHash(hashBytes, saltBytes);
    }

    public bool Verify(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            return false;

        var computedHash = HashPassword(plainPassword, this.Salt);

        return CryptographicOperations.FixedTimeEquals(this.Hash, computedHash);
    }

    public static PrimitiveResult IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return PrimitiveResult.Failure(ErrorCodes.Empty, "رمز عبور نمی‌تواند خالی باشد");

        if (value.Length < MinLength)
            return PrimitiveResult.Failure(ErrorCodes.TooShort, $"حداقل {MinLength} کاراکتر");

        if (value.Length > MaxLength)
            return PrimitiveResult.Failure(ErrorCodes.TooLong, $"حداکثر {MaxLength} کاراکتر");

        if (!value.Any(char.IsUpper) ||
            !value.Any(char.IsLower) ||
            !value.Any(char.IsDigit) ||
            !value.Any(ch => !char.IsLetterOrDigit(ch)))
            return PrimitiveResult.Failure(ErrorCodes.Weak, "رمز عبور ضعیف است");

        return PrimitiveResult.Success();
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        return StringHasherHelper.Get_Aragon2(password, salt);
    }

    private static string Normalize(string value)
        => value?.Trim() ?? string.Empty;

}
