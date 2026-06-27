using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class NationalCodeValueConverter : ValueConverter<NationalCode, string>
{
    public NationalCodeValueConverter() : base(
        src => src.Value,
        value => NationalCode.CreateUnsafe(value))
    { }
}

internal sealed class NationalCodeValueComparer : ValueComparer<NationalCode>
{
    public NationalCodeValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

