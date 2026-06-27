using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;
using System.Text.Json;


namespace ER.Melksaz.Modules.IdentityModule.Api.JsonConverters;

internal sealed class NationalCodeJsonConverter : ValueObjectJsonConverter<NationalCode>
{
    protected override PrimitiveResult TryParse(string? value, out NationalCode result)
    {
        var x = NationalCode.Create(value ?? string.Empty);

        if (x.IsSuccess)
        {
            result = x.Value;
            return PrimitiveResult.Success();
        }

        return PrimitiveResult.Failure(x.Errors);
    }

    public override void Write(
        Utf8JsonWriter writer,
        NationalCode value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}
