namespace Global.Http.Services;


public class Client
{


    /// <summary>
    /// Cliente HTTP.
    /// </summary>
    private readonly HttpClient HttpClient = new();


    /// <summary>
    /// Segundos del timeOut.
    /// </summary>
    public int TimeOut { get; set; } = 20;



    /// <summary>
    /// Parámetros.
    /// </summary>
    private readonly Dictionary<string, string> Parameters = [];




    /// <summary>
    /// Nuevo cliente http.
    /// </summary>
    /// <param name="url">Url base</param>
    public Client(string? url = null)
    {
        try
        {
           HttpClient.Timeout = TimeSpan.FromSeconds(7);

            if (url != null)
                HttpClient.BaseAddress = new Uri(url ?? "");
        }
        catch (Exception)
        {
        }
    }



    /// <summary>
    /// Construir parámetros.
    /// </summary>
    private void Build()
    {
        try
        {
            HttpClient.Timeout = TimeSpan.FromSeconds(TimeOut);
            string url = Global.Utilities.Network.Web.AddParameters(HttpClient.BaseAddress?.ToString() ?? "", Parameters);
            HttpClient.BaseAddress = new Uri(url);
        }
        catch (Exception)
        {
        }

    }




    /// <summary>
    /// Agregar parámetro a la url
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddParameter(string name, string value)
    {
        Parameters.Add(name, value);
    }



    /// <summary>
    /// Agregar parámetro a la url
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddParameter(string name, int value)
    {
        AddParameter(name, value.ToString());
    }


    /// <summary>
    /// Agregar parámetro a la url
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddParameter(string name, bool value)
    {
        AddParameter(name, value.ToString());
    }



    /// <summary>
    /// Agregar parámetro a la url
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddParameter(string name, DateTime value)
    {
        AddParameter(name, value.ToString("yyyy-MM-ddTHH:mm:ss"));
    }


   
    /// <summary>
    /// Agregar un header.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddHeader(string name, string value)
    {
        HttpClient.DefaultRequestHeaders.Add(name, value);
    }


    /// <summary>
    /// Agregar un header.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddHeader(string name, int value)
    {
        AddHeader(name, value.ToString());
    }


    /// <summary>
    /// Agregar un header.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="value">Valor</param>
    public void AddHeader(string name, bool value)
    {
        AddHeader(name, value.ToString());
    }


    

    /// <summary>
    /// Enviar solicitud [GET]
    /// </summary>
    public async Task<T> Get<T>() where T : class, new()
    {
        try
        {
            Build();

            // Resultado.
            var result = await HttpClient.GetAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;

        }
        catch (Exception)
        {
        }

        return new();
    }



    /// <summary>
    /// Enviar solicitud [GET]
    /// </summary>
    public async Task<string> Get()
    {
        try
        {
            Build();

            // Resultado.
            var result = await HttpClient.GetAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Respuesta.
            return response;

        }
        catch (Exception)
        {
        }

        return "";
    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Patch<T>(object? body = null) where T : class, new()
    {

        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PatchAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception)
        {
        }

        return new();

    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<string> Patch(object? body = null)
    {

        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PatchAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Respuesta.
            return response;
        }
        catch (Exception)
        {
        }

        return "";

    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Post<T>(object? body = null) where T : class, new()
    {
        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PostAsync("", content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception)
        {
        }

        return new();
    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<string> Post(object? body = null)
    {
        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PostAsync("", content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Respuesta.
            return response;
        }
        catch (Exception)
        {
        }

        return "";
    }




    /// <summary>
    /// Enviar solicitud [PUT]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Put<T>(object? body = null) where T : class, new()
    {
        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PutAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception)
        {
        }
        return new();

    }



    /// <summary>
    /// Enviar solicitud [PUT]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<string> Put(object? body = null)
    {
        try
        {
            Build();

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PutAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Respuesta.
            return response;
        }
        catch (Exception)
        {
        }
        return "";

    }



    /// <summary>
    /// Enviar solicitud [DELETE]
    /// </summary>
    public async Task<T> Delete<T>() where T : class, new()
    {

        try
        {
            Build();

            // Resultado.
            var result = await HttpClient.DeleteAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception)
        { }

        return new();
    }



    /// <summary>
    /// Enviar solicitud [DELETE]
    /// </summary>
    public async Task<string> Delete()
    {

        try
        {
            Build();

            // Resultado.
            var result = await HttpClient.DeleteAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            // Respuesta.
            return response;
        }
        catch (Exception)
        { }

        return "";
    }



    /// <summary>
    /// Obtener una respuesta.
    /// </summary>
    /// <typeparam name="T">Tipo de la respuesta.</typeparam>
    /// <param name="content">Contenido.</param>
    public static T Deserialize<T>(string content) where T : class, new()
    {
        try
        {
            // Objeto
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);


            return result ?? new();

        }
        catch (Exception)
        {
        }

        return new();
    }



    /// <summary>
    /// Establecer la URL base.
    /// </summary>
    /// <param name="url">Nueva URL.</param>
    public void SetBaseAddress(string url) => SetBaseAddress(new Uri(url));
    



    /// <summary>
    /// Establecer la URL base.
    /// </summary>
    /// <param name="url">Nueva URL.</param>
    public void SetBaseAddress(Uri url) => HttpClient.BaseAddress = url;
  


}