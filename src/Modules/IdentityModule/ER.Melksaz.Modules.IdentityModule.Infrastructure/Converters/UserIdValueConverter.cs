using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class UserIdValueConverter : ValueConverter<UserId, string>
{
    public UserIdValueConverter() : base(
        src => src.Value,
        value => new UserId(value))
    { }
}

internal sealed class UserIdValueComparer : ValueComparer<UserId>
{
    public UserIdValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

