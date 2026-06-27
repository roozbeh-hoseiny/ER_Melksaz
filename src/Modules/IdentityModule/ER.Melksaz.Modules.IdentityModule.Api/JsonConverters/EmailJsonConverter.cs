using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;
using System.Text.Json;


namespace ER.Melksaz.Modules.IdentityModule.Api.JsonConverters;

internal sealed class EmailJsonConverter : ValueObjectJsonConverter<Email>
{
    public override void Write(
        Utf8JsonWriter writer,
        Email value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.Value);

    protected override PrimitiveResult TryParse(string? value, out Email result)
    {
        var x = Email.Create(value ?? string.Empty);

        if (x.IsSuccess)
        {
            result = x.Value;
            return PrimitiveResult.Success();
        }

        return PrimitiveResult.Failure(x.Errors);
    }
}
