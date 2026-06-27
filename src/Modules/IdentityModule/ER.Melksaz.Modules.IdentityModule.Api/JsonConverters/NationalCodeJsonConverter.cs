using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ER.Melksaz.Modules.IdentityModule.Api.JsonConverters;

internal sealed class NationalCodeJsonConverter : JsonConverter<NationalCode>
{
    public override NationalCode Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var result = NationalCode.Create(reader.GetString()!);

        if (result.IsSuccess) return result.Value;

        throw new JsonException(result.Error.Message);
    }

    public override void Write(
        Utf8JsonWriter writer,
        NationalCode value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}
