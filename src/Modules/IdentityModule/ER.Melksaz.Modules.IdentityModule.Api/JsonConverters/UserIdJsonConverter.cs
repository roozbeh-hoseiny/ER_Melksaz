using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ER.Melksaz.Modules.IdentityModule.Api.JsonConverters;

internal sealed class UserIdJsonConverter : JsonConverter<UserId>
{
    public override UserId Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) => new UserId(reader.GetString()!);

    public override void Write(
        Utf8JsonWriter writer,
        UserId value,
        JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
}
