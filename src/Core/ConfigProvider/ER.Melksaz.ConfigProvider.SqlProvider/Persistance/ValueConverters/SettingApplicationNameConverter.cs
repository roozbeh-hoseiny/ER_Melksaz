using Dapper;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using System.Data;

namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueConverters;

public sealed class SettingApplicationNameConverter : SqlMapper.TypeHandler<SettingApplicationName>
{
    public override SettingApplicationName Parse(object value) => SettingApplicationName.CreateUnsafe(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingApplicationName value)
    {
        throw new NotImplementedException();
    }
}
