using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ER.Melksaz.ConfigProvider.SqlProvider.Persistance;
internal sealed class JsonConfigurationParser
{
    private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    private readonly Stack<string> _path = new Stack<string>();
    private string _currentPath = string.Empty;
    private JsonTextReader _jsonTextReader = null!;

    private JsonConfigurationParser() { }

    public static IDictionary<string, string> Parse(string json)
    {
        return new JsonConfigurationParser().ParseJsonTree(json);
    }

    private IDictionary<string, string> ParseJsonTree(string json)
    {
        this._data.Clear();

        this._jsonTextReader = new JsonTextReader(new StringReader(json));
        this._jsonTextReader.DateParseHandling = DateParseHandling.None;

        JObject jsonConfig = JObject.Load(this._jsonTextReader);

        this.VisitInternal(jsonConfig);

        return this._data;
    }

    private void VisitInternal(JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                this.VisitInternal(token.Value<JObject>());
                break;
            case JTokenType.Array:
                this.VisitInternal(token.Value<JArray>());
                break;
            case JTokenType.Integer:
            case JTokenType.Float:
            case JTokenType.String:
            case JTokenType.Boolean:
            case JTokenType.Bytes:
            case JTokenType.Raw:
            case JTokenType.Null:
                this.VisitLeaf(token.Value<JValue>());
                break;
            default:
                throw new FormatException($"Can not parse json of {this._jsonTextReader.TokenType} from {this._jsonTextReader.Path} at {this._jsonTextReader.LineNumber} line and {this._jsonTextReader.LinePosition}.");
        }
    }

    private void VisitInternal(JArray? array)
    {
        if (array is null) return;
        for (int index = 0; index < array.Count; index++)
        {
            this.GoDescendant(index.ToString());
            this.VisitInternal(array[index]);
            this.GoAncestor();
        }
    }

    private void VisitInternal(JObject? jObject)
    {
        if (jObject is null) return;
        foreach (var property in jObject.Properties())
        {
            this.GoDescendant(property.Name);
            this.VisitInternal(property.Value);
            this.GoAncestor();
        }
    }


    private void VisitLeaf(JValue? data)
    {
        if (data is null) return;
        var key = this._currentPath;

        if (this._data.ContainsKey(key))
        {
            throw new FormatException($"Can not parse json. Duplicate key: {key}");
        }
        this._data[key] = data.ToString(CultureInfo.InvariantCulture);
    }

    private void GoDescendant(string context)
    {
        this._path.Push(context);
        this._currentPath = ConfigurationPath.Combine(this._path.Reverse());
    }

    private void GoAncestor()
    {
        this._path.Pop();
        this._currentPath = ConfigurationPath.Combine(this._path.Reverse());
    }
}
