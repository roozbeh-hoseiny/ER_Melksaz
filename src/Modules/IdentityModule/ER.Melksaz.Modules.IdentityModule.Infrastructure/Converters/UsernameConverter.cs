using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class UsernameValueConverter : ValueConverter<Username, string>
{
    public UsernameValueConverter() : base(
        src => src.Value,
        value => Username.CreateUnsafe(value))
    { }
}

internal sealed class UsernameValueComparer : ValueComparer<Username>
{
    public UsernameValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

