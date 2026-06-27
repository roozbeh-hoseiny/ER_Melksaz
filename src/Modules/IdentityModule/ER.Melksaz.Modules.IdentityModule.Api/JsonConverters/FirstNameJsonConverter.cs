using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ER.Melksaz.Modules.IdentityModule.Api.JsonConverters;

internal sealed class FirstNameJsonConverter : JsonConverter<FirstName>
{
    public override FirstName Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var result = FirstName.Create(reader.GetString()!);

        if (result.IsSuccess) return result.Value;

        throw new JsonException(result.Error.Message);
    }

    public override void Write(
        Utf8JsonWriter writer,
        FirstName value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}
