namespace ER.Melksaz.Security.Encryption;

public sealed record EncryptionResult(string EncryptedValue, string EncryptKeyId)
{
    public string GetValue() => $"{this.EncryptedValue}|{this.EncryptKeyId}";
    public string GetBase64Value() => System.Convert.ToBase64String(
        System.Text.Encoding.UTF8.GetBytes(
        this.GetValue()));
}
