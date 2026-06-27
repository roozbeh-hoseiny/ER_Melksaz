namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;

public readonly record struct SettingKey(string Value)
{
    public SettingKey() : this(string.Empty) { }

    public static SettingKey CreateUnsafe(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new InvalidDataException("SettingKey can not be empty");
        return new SettingKey(key);
    }
}
