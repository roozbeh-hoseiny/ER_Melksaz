using Dapper;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.Entities;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace ER.Melksaz.ConfigProvider.SqlProvider;

public sealed class AvashDbConfigProvider : ConfigurationProvider, IDisposable
{
    #region " Fields "
    private bool _disposed;
    private readonly string _connectionString;
    private readonly AvashDbConfigSource _configurationSource;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private byte[] _lastComputedHash = [];
    private Task? _watchDbTask = null;
    #endregion

    public AvashDbConfigProvider(
        string connectionString,
        AvashDbConfigSource configurationSource)
    {
        this._connectionString = connectionString;
        this._configurationSource = configurationSource;
        this._cancellationTokenSource = new CancellationTokenSource();
    }

    public override void Load()
    {
        if (this._watchDbTask is not null) return;

        var data = this.GetData()!;
        this._lastComputedHash = this.ComputeHash(data!);
        this.Data = this.BuildSettingsDictionary(data!);
        var ct = this._cancellationTokenSource.Token;
        if (this._configurationSource.ReloadOnChange)
        {
            this._watchDbTask = Task.Run(() => this.WatchDatabase(ct), ct);
        }
    }

    private IDictionary<string, string> GetData()
    {
        var settings = this.GetSettings(
            this._configurationSource.ApplicationName,
            this._configurationSource.Version,
            this._configurationSource.Environment);

        return settings?.ToDictionary(
                s => s.Key.Value,
                s => s.Value, StringComparer.OrdinalIgnoreCase
            ) ?? new(StringComparer.OrdinalIgnoreCase);
    }
    private async Task<IDictionary<string, string>> GetDataAsync(CancellationToken cancellationToken)
    {
        var settings = await this.GetSettingsAsync(
            this._configurationSource.ApplicationName,
            this._configurationSource.Version,
            this._configurationSource.Environment, cancellationToken)
            .ConfigureAwait(false);

        return settings?.ToDictionary(
                s => s.Key.Value,
                s => s.Value, StringComparer.OrdinalIgnoreCase
            ) ?? new(StringComparer.OrdinalIgnoreCase);
    }
    private byte[] ComputeHash(IDictionary<string, string> dict)
    {
        List<byte> byteDict = [];
        foreach (KeyValuePair<string, string> item in dict)
        {
            byteDict.AddRange(Encoding.Unicode.GetBytes($"{item.Key}{item.Value}"));
        }

        return System.Security.Cryptography.SHA1.Create().ComputeHash(byteDict.ToArray());
    }
    private async Task WatchDatabase(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(this._configurationSource.PollingInterval);
            var data = await this.GetDataAsync(cancellationToken).ConfigureAwait(false);
            var hash = this.ComputeHash(data!);
            if (!hash.SequenceEqual(this._lastComputedHash))
            {
                this._lastComputedHash = hash;
                System.Diagnostics.Debug.WriteLine("Some changes affected in settings");
                this.Data = this.BuildSettingsDictionary(data!);
                this.OnReload();
            }
        }
    }
    private IDictionary<string, string?> BuildSettingsDictionary(IDictionary<string, string?> settingsDictionary)
    {
        return settingsDictionary;
    }

    private Settings[] GetSettings(SettingApplicationName appName, SettingVersion ver, string env)
    {
        var result = new List<Settings>();

        foreach (var item in this.ExecuteReader<Settings>(
            DapperCommandDefinitionBuilder
                .StreamedProcedure("Settings_Get")
                .SetParameter("appname", appName.Value)
                .SetParameter("version", ver.GetDbValue())
                .SetParameter("env", env)
                .Build()))
        {
            if (item is null) continue;
            result.Add(item!);
        }
        return result.ToArray();
    }
    private IEnumerable<T> ExecuteReader<T>(CommandDefinition command)
    {
        SqlConnection? connection = null;
        IDataReader? reader = null;

        try
        {
            connection = new SqlConnection(this._connectionString);
            connection.Open();
            reader = connection.ExecuteReader(command);

            if (reader is null) yield break;
            var rowParser = reader.GetRowParser<T>();

            while (reader.Read())
            {
                yield return rowParser(reader);
            }
        }
        finally
        {
            reader?.Dispose();

            connection?.Dispose();
        }
    }

    private async Task<Settings[]> GetSettingsAsync(
        SettingApplicationName appName,
        SettingVersion ver,
        string env,
        CancellationToken cancellationToken)
    {
        var result = new List<Settings>();

        await foreach (var item in this.ExecuteReaderAsync<Settings>(
            DapperCommandDefinitionBuilder
                .StreamedProcedure("Settings_Get")
                .SetParameter("appname", appName.Value)
                .SetParameter("version", ver.GetDbValue())
                .SetParameter("env", env)
                .Build(), cancellationToken))
        {
            if (item is null) continue;
            result.Add(item!);
        }
        return result.ToArray();
    }
    private async IAsyncEnumerable<T> ExecuteReaderAsync<T>(
        CommandDefinition command,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        SqlConnection? connection = null;
        IDataReader? reader = null;

        try
        {
            connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            reader = await connection.ExecuteReaderAsync(command).ConfigureAwait(false);

            if (reader is null) yield break;
            var rowParser = reader.GetRowParser<T>();

            while (reader.Read())
            {
                if (cancellationToken.IsCancellationRequested) yield break;
                yield return rowParser(reader);
            }
        }
        finally
        {
            reader?.Dispose();

            _ = (connection?.DisposeAsync());
        }
    }

    public void Dispose()
    {
        if (this._disposed)
        {
            return;
        }

        this._cancellationTokenSource.Cancel();
        this._cancellationTokenSource.Dispose();
        this._disposed = true;
    }

}
