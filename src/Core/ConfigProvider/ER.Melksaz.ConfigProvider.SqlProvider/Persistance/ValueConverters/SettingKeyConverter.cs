using Dapper;
using ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueObjects;
using System.Data;

namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance.ValueConverters;

public sealed class SettingKeyConverter : SqlMapper.TypeHandler<SettingKey>
{
    public override SettingKey Parse(object value) => SettingKey.CreateUnsafe(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingKey value)
    {
        throw new NotImplementedException();
    }
}
