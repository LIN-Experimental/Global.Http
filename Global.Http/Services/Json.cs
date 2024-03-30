namespace Global.Http.Services;

internal class Json
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



}