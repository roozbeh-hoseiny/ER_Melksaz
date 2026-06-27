using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace ER.Melksaz.ConfigProvider.SqlProvider;

public sealed class AvashDbConfigSource : IConfigurationSource
{
    #region " Fields "
    private readonly string _connectionString;
    private readonly bool _reloadOnChange;
    private readonly string _environment;
    private readonly SettingApplicationName _applicationName;
    private readonly SettingVersion _version;
    private readonly TimeSpan _pollingInterval;
    #endregion

    #region " Properties "
    public string Environment => this._environment;
    public SettingApplicationName ApplicationName => this._applicationName;
    public SettingVersion Version => this._version;
    public TimeSpan PollingInterval => this._pollingInterval;
    public bool ReloadOnChange => this._reloadOnChange;
    #endregion

    public AvashDbConfigSource(
        string connectionString,
        bool reloadOnChange,
        string environment,
        SettingApplicationName applicationName,
        SettingVersion version,
        TimeSpan pollingInterval)
    {
        this._connectionString = connectionString;
        this._reloadOnChange = reloadOnChange;
        this._environment = environment;
        this._applicationName = applicationName;
        this._version = version;
        this._pollingInterval = pollingInterval;
    }


    public IConfigurationProvider Build(IConfigurationBuilder builder) => new AvashDbConfigProvider(this._connectionString, this);
}
