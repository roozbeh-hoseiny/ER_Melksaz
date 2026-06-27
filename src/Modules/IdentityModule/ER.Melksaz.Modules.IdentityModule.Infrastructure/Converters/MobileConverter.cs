using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class MobileValueConverter : ValueConverter<Mobile, string>
{
    public MobileValueConverter() : base(
        src => src.Value,
        value => Mobile.CreateUnsafe(value))
    { }
}

internal sealed class MobileValueComparer : ValueComparer<Mobile>
{
    public MobileValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

