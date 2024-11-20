using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Global.Http.Services;

public class Json
{

    /// <summary>
    /// Serializar objeto.
    /// </summary>
    /// <param name="obj">Objeto</param>
    public static string Serialize(object? obj)
    {
        try
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        catch (Exception)
        {
        }
        return string.Empty;
    }



    public static T? Deserialize<T, U>(string json, Dictionary<string, Type> keys)
    {
        try
        {

            if (keys == null)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            var settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new PolimorfismConverter<U>(keys, typeof(U)) }
            };

            var result = JsonConvert.DeserializeObject<T>(json, settings);

            return result;



        }
        catch (Exception)
        {
        }
        return default;
    }



}

public class PolimorfismConverter<C> : JsonConverter
{
    private readonly Dictionary<string, Type> _typeMappings;
    public readonly Type type;

    public PolimorfismConverter(Dictionary<string, Type> typeMappings, Type type)
    {
        _typeMappings = typeMappings;
        this.type = type;
    }

    public override bool CanConvert(Type objectType)
    {
        return type.IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        string type = jo["type"]?.ToString();

        if (type != null && _typeMappings.ContainsKey(type))
        {
            var targetType = _typeMappings[type];
            // Deserialize the object based on the type
            return jo.ToObject(targetType);
        }

        // If Type is not found or invalid, just return a basic Animal
        return jo.ToObject<C>(serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();  // Not needed for deserialization
    }
}