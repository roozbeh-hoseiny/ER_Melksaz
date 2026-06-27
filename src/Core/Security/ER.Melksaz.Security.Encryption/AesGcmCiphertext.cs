using System.Security.Cryptography;

namespace ER.Melksaz.Security.Encryption;

internal sealed class AesGcmCiphertext
{
    public byte[] Nonce { get; }
    public byte[] Tag { get; }
    public byte[] CiphertextBytes { get; }

    public static AesGcmCiphertext FromBase64String(string data)
    {
        var dataBytes = Convert.FromBase64String(data);
        return new AesGcmCiphertext(
            dataBytes.Take(AesGcm.NonceByteSizes.MaxSize).ToArray(),
            dataBytes[^AesGcm.TagByteSizes.MaxSize..],
            dataBytes[AesGcm.NonceByteSizes.MaxSize..^AesGcm.TagByteSizes.MaxSize]
        );
    }

    public AesGcmCiphertext(byte[] nonce, byte[] tag, byte[] ciphertextBytes)
    {
        this.Nonce = nonce;
        this.Tag = tag;
        this.CiphertextBytes = ciphertextBytes;
    }

    public override string ToString()
    {
        return Convert.ToBase64String(this.Nonce.Concat(this.CiphertextBytes).Concat(this.Tag).ToArray());
    }
}