using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class LastNameValueConverter : ValueConverter<LastName, string>
{
    public LastNameValueConverter() : base(
        src => src.Value,
        value => LastName.CreateUnsafe(value))
    { }
}

internal sealed class LastNameValueComparer : ValueComparer<LastName>
{
    public LastNameValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

