using ER.Melksaz.PrimitiveResults;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ER.Melksaz.BuildingBlocks.Api;

public abstract class ValueObjectJsonConverter<T> : JsonConverter<T>
{
    protected abstract PrimitiveResult TryParse(
        string? value,
        out T result);

    public override T Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        string? errorMessage;
        try
        {
            var value = reader.GetString();

            var parseResult = this.TryParse(value, out var result);
            if (parseResult.IsSuccess)
            {
                return result;
            }
            errorMessage = parseResult.Error.Message;
        }
        catch (Exception)
        {
            errorMessage = $"Bad request for type : '{typeof(T).Name}'";
        }
        throw new JsonException(errorMessage);
    }
}