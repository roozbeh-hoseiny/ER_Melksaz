using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ER.Melksaz.Modules.IdentityModule.Infrastructure.Converters;

internal sealed class EmailValueConverter : ValueConverter<Email, string>
{
    public EmailValueConverter() : base(
        src => src.Value,
        value => Email.CreateUnsafe(value))
    { }
}

internal sealed class EmailValueComparer : ValueComparer<Email>
{
    public EmailValueComparer() : base(
        (a, b) => a.Value.Equals(b.Value, StringComparison.InvariantCultureIgnoreCase),
        a => a.Value.GetHashCode())
    { }
}

