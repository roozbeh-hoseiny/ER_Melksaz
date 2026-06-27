namespace ER.Melksaz.Security.Encryption;

public sealed class EncryptionServiceKeyItem
{
    private byte[]? _key = null;
    public string Id { get; set; } = string.Empty;
    public byte[] Key
    {
        get
        {
            if (this._key is null || this._key.Length == 0)
            {
                this._key = System.Text.Encoding.UTF8.GetBytes(this.KeyString);
            }
            return this._key;
        }
        set
        {
            this._key = value;
        }
    }
    public int Order { get; set; }
    public string KeyString { get; set; } = string.Empty;
}
