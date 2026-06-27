namespace ER.Melksaz.Security.Encryption;

public sealed class EncryptionServiceOptions
{
    public string HashKey { get; set; } = "r7!KzT3hG4mV8pQ1@wE2yL9#sB5uN0x";
    public EncryptionServiceKeyItem[] Keys { get; set; } = [];
}
