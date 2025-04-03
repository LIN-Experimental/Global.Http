using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

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
            return JsonConvert.SerializeObject(obj);
        }
        catch (Exception)
        {
        }
        return string.Empty;
    }



    public static T? Deserialize<T, U>(string json, Dictionary<string, Type> keys, string property)
    {
        try
        {

            if (keys == null)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            var settings = new JsonSerializerSettings
            {
                Converters = [new PolimorfismConverter<U>(keys, typeof(U), property)]
            };

            var result = JsonConvert.DeserializeObject<T>(json, settings);

            return result;



        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error JSON: {ex.Message} - {ex.StackTrace}");
        }
        return default;
    }



}

public class PolimorfismConverter<C> : JsonConverter
{
    private readonly Dictionary<string, Type> _typeMappings;
    public readonly Type type;
    public readonly string name;

    public PolimorfismConverter(Dictionary<string, Type> typeMappings, Type type, string field)
    {
        _typeMappings = typeMappings;
        this.type = type;
        this.name = field;
    }

    public override bool CanConvert(Type objectType)
    {
        return type.IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // Cargar el objeto.
        JObject @object = JObject.Load(reader);

        // Validar si existe la propiedad.
        JToken? value = @object.Properties()
                               .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

        // Obtener la key de la propiedad.
        var key = _typeMappings.FirstOrDefault(t => t.Key.ToLower() == value?.Last?.ToString()?.ToLower());

        // Si existe la propiedad, deserializamos.
        if (value is not null && key.Key is not null && key.Value is not null)
        {
            // Deserialize the object based on the type
            return @object.ToObject(key.Value) ?? null!;
        }

        // Si es un tipo primitivo o no es un objeto, usar deserialización directa
        if (objectType.IsPrimitive || objectType == typeof(string) || objectType == typeof(decimal) || (type == null && objectType == this.type))
        {
            return @object.ToObject(objectType) ?? null!;
        }

        // If Type is not found or invalid, just return a basic Animal
        return @object.ToObject(objectType) ?? null!;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();  // Not needed for deserialization
    }
}