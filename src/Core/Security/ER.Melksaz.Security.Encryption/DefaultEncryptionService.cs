using ER.Melksaz.PrimitiveResults;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ER.Melksaz.Security.Encryption;

public sealed class DefaultEncryptionService : IEncryptionService
{
    private readonly IOptionsMonitor<EncryptionServiceOptions> _opts;
    private readonly byte[] _hashKeyBytes = [];
    public DefaultEncryptionService(IOptionsMonitor<EncryptionServiceOptions> opts)
    {
        this._opts = opts;
        this._hashKeyBytes = Encoding.UTF8.GetBytes(this._opts.CurrentValue.HashKey);
    }

    public ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(byte[] src) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(this.EncryptCore(src)));
    public ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(string src) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(this.EncryptCore(Encoding.UTF8.GetBytes(src))));

    public ValueTask<PrimitiveResult<string>> EncryptValue(string src) =>
        this.Encrypt(src).Map(x => x.GetValue());
    public string UnsafeEncrypt(string src) => this.EncryptCore(Encoding.UTF8.GetBytes(src)).GetValue();
    public string UnsafeDecrypt(string src)
    {
        var parts = src.Split('|');
        return Encoding.UTF8.GetString(this.DecryptCore(parts[0], parts[1]));
    }


    public ValueTask<PrimitiveResult<byte[]>> Decrypt(string src, string id) =>
        ValueTask.FromResult(PrimitiveResult.Success(this.DecryptCore(src, id)));
    public ValueTask<PrimitiveResult<string>> Decrypt(string src)
    {
        var parts = src.Split('|');
        if (parts.Length < 2) return ValueTask.FromResult(PrimitiveResult.InternalFailure<string>("", "Invalid input for decryption"));
        return this.Decrypt(parts[0], parts[1])
            .Map(Encoding.UTF8.GetString);
    }
    public ValueTask<PrimitiveResult<string>> Decrypt(byte[] src) => this.Decrypt(this.ConvertToString(src));
    public ValueTask<PrimitiveResult<string>> DeterministicHash(string src)
    {
        using var hmac = new HMACSHA256(this._hashKeyBytes);
        var bytes = Encoding.UTF8.GetBytes(src.ToLowerInvariant().Trim());
        var hashBytes = hmac.ComputeHash(bytes);
        return ValueTask.FromResult(PrimitiveResult.Success(Convert.ToHexString(hashBytes))); // or Base64 
    }


    private EncryptionServiceKeyItem GetLastKeyItem(EncryptionServiceKeyItem[] src) => src.OrderByDescending(x => x.Order).First();
    private EncryptionServiceKeyItem GetKeyById(EncryptionServiceKeyItem[] src, string id) => src.First(a => a.Id.Equals(id));
    private string ConvertToString(byte[] src) => Convert.ToBase64String(src);
    private EncryptionResult EncryptCore(byte[] src)
    {
        if (src.Length == 0)
        {
            return new EncryptionResult(
                    string.Empty,
                    "0");
        }
        var keyItem = this.GetLastKeyItem(this._opts.CurrentValue.Keys);
        using var aes = new AesCcm(keyItem.Key);

        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);
        var plaintextBytes = src;
        var ciphertextBytes = new byte[plaintextBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aes.Encrypt(nonce, plaintextBytes, ciphertextBytes, tag);
        return new EncryptionResult(
                    new AesGcmCiphertext(nonce, tag, ciphertextBytes).ToString(),
                    keyItem.Id);
    }
    private byte[] DecryptCore(string src, string id)
    {
        if (string.IsNullOrEmpty(src) && id.Equals("0")) return Array.Empty<byte>();

        var keyItem = this.GetKeyById(this._opts.CurrentValue.Keys, id);
        var gcmCiphertext = AesGcmCiphertext.FromBase64String(src);
        using var aes = new AesCcm(keyItem.Key);
        var plaintextBytes = new byte[gcmCiphertext.CiphertextBytes.Length];
        aes.Decrypt(gcmCiphertext.Nonce, gcmCiphertext.CiphertextBytes, gcmCiphertext.Tag, plaintextBytes);
        return plaintextBytes;
    }

}
