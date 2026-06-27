using Dapper;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using System.Data;

namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueConverters;

public sealed class SettingVersionConverter : SqlMapper.TypeHandler<SettingVersion>
{
    public override SettingVersion Parse(object value) => SettingVersion.CreateUnsafe(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingVersion value)
    {
        throw new NotImplementedException();
    }
}
