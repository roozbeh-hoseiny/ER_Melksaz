namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;

public readonly record struct SettingVersion(
    int Major,
    int Minor,
    int Patch,
    int Revision)
{
    public static readonly SettingVersion AllVersions = new SettingVersion(0, 0, 0, 0);
    public static readonly SettingVersion Version1 = new SettingVersion(1, 0, 0, 0);

    public SettingVersion() : this(0, 0, 0, 0) { }

    public string GetDbValue() => $"{this.Major}.{this.Minor}.{this.Patch}{(this.Revision > 0 ? $".{this.Revision.ToString().PadLeft(6, '0')}" : $".0")}";

    public static SettingVersion CreateUnsafe(string version)
    {
        var parts = version.Split('.');
        if (parts.Length < 3) return Version1;

        return new SettingVersion(
            Convert.ToInt32(parts[0]),
            Convert.ToInt32(parts[1]),
            Convert.ToInt32(parts[2]),
            Convert.ToInt32(parts[3]));
    }
}
