using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class FirstNameValueConverter : ValueConverter<FirstName, string>
{
    public FirstNameValueConverter() : base(
        src => src.Value,
        value => FirstName.CreateUnsafe(value))
    { }
}

internal sealed class FirstNameValueComparer : ValueComparer<FirstName>
{
    public FirstNameValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

