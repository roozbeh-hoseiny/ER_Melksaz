using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace ER.Melksaz.BuildingBlocks.Helpers;

public static class StringHasherHelper
{
    public static string Get_MD5(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        using (var hasher = MD5.Create())
        {
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder();
            foreach (var b in bytes)
                _ = builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
    public static string GetDeterministicHash_MD5(string input) => Get_MD5(input.ToLowerInvariant());

    public static string Get_SHA256(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        using (var hasher = SHA256.Create())
        {
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            var builder = new StringBuilder();
            foreach (var b in bytes)
                _ = builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
    public static string GetDeterministicHash_SHA256(string input) => Get_SHA256(input.ToLowerInvariant());



    public static byte[] Get_HMACSHA256(string input, string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        if (string.IsNullOrWhiteSpace(input)) return Array.Empty<byte>();

        return HMACSHA256.HashData(
            Encoding.UTF8.GetBytes(key),
            Encoding.UTF8.GetBytes(input));

    }
    public static string GetDeterministicHash_HMACSHA256(string input, string key)
    {
        return Convert.ToHexString(Get_HMACSHA256(input.ToLowerInvariant(), key));
    }

    private static byte[] Get_Aragon2_Core(string input, byte[] key)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(input))
        {
            Salt = key,
            Iterations = 4,
            MemorySize = 65536, // 64 MB
            DegreeOfParallelism = 2
        };
        return argon2.GetBytes(32);
    }
    public static string GetDeterministicHash_Aragon2(string input, string key)
    {
        return Convert.ToHexString(Get_Aragon2_Core(input.ToLowerInvariant(), Encoding.UTF8.GetBytes(key)));
    }
    public static byte[] Get_Aragon2(string input, byte[] key)
    {
        return Get_Aragon2_Core(input, key);
    }
}
