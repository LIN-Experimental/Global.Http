using System.IO;
using System.Net.Http.Headers;

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
            HttpClient.Timeout = TimeSpan.FromSeconds(10);

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
            string url = Utilities.Network.Web.AddParameters(HttpClient.BaseAddress?.ToString() ?? "", Parameters);
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
    /// Construir salida.
    /// </summary>
    /// <param name="method">Método.</param>
    /// <param name="body">Body.</param>
    public void BuildOutput(string method, object? body = null)
    {

        try
        {
            Build();
        }
        catch (Exception ex)
        {
          
        }
    }

    private void Out(System.Net.HttpStatusCode code, string message)
    {
     
    }

    /// <summary>
    /// Enviar solicitud [GET]
    /// </summary>
    public async Task<T> Get<T>() where T : class, new()
    {
        try
        {
            BuildOutput("GET");

            // Resultado.
            var result = await HttpClient.GetAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();
    }


    /// <summary>
    /// Enviar solicitud [GET]
    /// </summary>
    public async Task<T> Get<T, U>(Dictionary<string, Type> pairs, string property = "type_data") where T : class, new()
    {
        try
        {
            BuildOutput("GET");

            // Resultado.
            var result = await HttpClient.GetAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T, U>(response, pairs, property);

            // Respuesta.
            return @object;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            BuildOutput("GET");

            // Resultado.
            var result = await HttpClient.GetAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Respuesta.
            return response;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return "";
    }


    /// <summary>
    /// Enviar solicitud [PATCH]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Patch<T>(object? body = null) where T : class, new()
    {

        try
        {
            BuildOutput("PATCH");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");


            // Crear la solicitud PATCH
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), string.Empty)
            {
                Content = content
            };

            var result = await HttpClient.SendAsync(request);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();

    }


    /// <summary>
    /// Enviar solicitud [PATH]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Patch<T, U>(object? body = null, Dictionary<string, Type>? types = null, string property = "type_data") where T : class, new()
    {

        try
        {
            BuildOutput("PATCH");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");


            // Crear la solicitud PATCH
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), string.Empty)
            {
                Content = content
            };

            var result = await HttpClient.SendAsync(request);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T, U>(response, types, property);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();

    }


    /// <summary>
    /// Enviar solicitud [PATCH]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<string> Patch(object? body = null)
    {

        try
        {
            BuildOutput("PATCH");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.

            // Crear la solicitud PATCH
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), string.Empty)
            {
                Content = content
            };

            var result = await HttpClient.SendAsync(request);



            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);
            // Respuesta.
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            BuildOutput("POST");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PostAsync("", content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();
    }


    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Post<T, U>(object? body = null, Dictionary<string, Type>? types = null, string property = "type_data") where T : class, new()
    {
        try
        {
            BuildOutput("POST");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PostAsync("", content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T, U>(response, types, property);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            BuildOutput("POST");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PostAsync("", content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Respuesta.
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        return "";
    }


    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <typeparam name="T">Genérico.</typeparam>
    /// <param name="stream">Stream.</param>
    /// <param name="name">Nombre del archivo.</param>
    /// <param name="onLoad">Acción al cargar.</param>
    public async Task<T> Post<T>(Stream stream, string name, Action<double> onLoad) where T : class, new()
    {
        try
        {
            BuildOutput("POST");

            // Obtener el tamaño total.
            long totalBytes = stream.Length;

            // Crear un Stream personalizado para rastrear el progreso.
            var progressStream = new ProgressStream(stream, totalBytes, (sentBytes, totalBytes) =>
            {
                onLoad.Invoke((double)sentBytes / totalBytes * 100);
            });

            // Cliente.
            using var content = new MultipartFormDataContent();

            // Configuración.
            var streamContent = new StreamContent(progressStream);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

            // Nombre del archivo
            content.Add(streamContent, "modelo", name);

            // Enviar la solicitud POST.
            var result = await HttpClient.PostAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();
    }


    /// <summary>
    /// Enviar solicitud [PUT]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Put<T>(object? body = null) where T : class, new()
    {
        try
        {
            BuildOutput("PUT");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PutAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();

    }


    /// <summary>
    /// Enviar solicitud [PUT]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Put<T, U>(object? body = null, Dictionary<string, Type>? types = null, string property = "type_data") where T : class, new()
    {
        try
        {
            BuildOutput("PUT");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PutAsync(string.Empty, content);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);

            // Objeto
            T @object = Deserialize<T, U>(response, types, property);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            BuildOutput("PUT");

            // Body en JSON.
            string json = Json.Serialize(body);

            // Contenido.
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Resultado.
            var result = await HttpClient.PutAsync(string.Empty, content);


            // Respuesta
            var response = await result.Content.ReadAsStringAsync();

            Out(result.StatusCode, response);

            // Respuesta.
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            BuildOutput("DELETE");

            // Resultado.
            var result = await HttpClient.DeleteAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);
            // Objeto
            T @object = Deserialize<T>(response);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        return new();
    }


    /// <summary>
    /// Enviar solicitud [DELETE]
    /// </summary>
    public async Task<string> Delete()
    {

        try
        {
            BuildOutput("DELETE");

            // Resultado.
            var result = await HttpClient.DeleteAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);
            // Respuesta.
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return "";
    }


    /// <summary>
    /// Enviar solicitud [DELETE]
    /// </summary>
    public async Task<T> Delete<T, U>(Dictionary<string, Type>? types = null, string property = "type_data") where T : class, new()
    {

        try
        {
            BuildOutput("DELETE");

            // Resultado.
            var result = await HttpClient.DeleteAsync(string.Empty);

            // Respuesta
            var response = await result.Content.ReadAsStringAsync();
            Out(result.StatusCode, response);
            // Objeto
            T @object = Deserialize<T, U>(response, types, property);

            // Respuesta.
            return @object;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        return new();
    }


    /// <summary>
    /// Obtener una respuesta.
    /// </summary>
    /// <typeparam name="T">Tipo de la respuesta.</typeparam>
    /// <param name="content">Contenido.</param>
    public static T Deserialize<T, U>(string content, Dictionary<string, Type>? types, string property) where T : class, new()
    {
        try
        {

            T? result = null;

            if (types is null)
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            else
                result = Json.Deserialize<T?, U?>(content, types, property);

            return result ?? new();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();
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

            T? result = null;

            result = Json.Deserialize<T?, T?>(content, null, string.Empty);

            return result ?? new();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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