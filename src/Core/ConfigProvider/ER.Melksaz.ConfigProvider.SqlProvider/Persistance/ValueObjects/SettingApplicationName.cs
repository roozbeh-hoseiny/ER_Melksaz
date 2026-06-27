namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;

public readonly record struct SettingApplicationName(string Value)
{
    public static readonly SettingApplicationName AllApplications = new SettingApplicationName("*");

    public SettingApplicationName() : this(string.Empty) { }
    public static SettingApplicationName CreateUnsafe(string appName)
    {
        if (string.IsNullOrWhiteSpace(appName)) return AllApplications;
        return new SettingApplicationName(appName.Trim());
    }
}
