using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Security.Encryption;

public interface IEncryptionService
{
    ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(byte[] src);
    ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(string src);
    ValueTask<PrimitiveResult<string>> EncryptValue(string src);
    ValueTask<PrimitiveResult<byte[]>> Decrypt(string src, string id);
    ValueTask<PrimitiveResult<string>> Decrypt(string src);
    ValueTask<PrimitiveResult<string>> Decrypt(byte[] src);
    ValueTask<PrimitiveResult<string>> DeterministicHash(string src);

    string UnsafeEncrypt(string src);
    string UnsafeDecrypt(string src);
}
