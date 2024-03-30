using Global.Http.Services;

namespace Global.Http;


public class Service
{


    /// <summary>
    /// Url base.
    /// </summary>
    private string DefaultUrl { get; set; } = string.Empty;



    /// <summary>
    /// Obtiene la Url.
    /// </summary>
    public string Url => DefaultUrl;



    /// <summary>
    /// Obtener un cliente Http.
    /// </summary>
    public Client GetClient(string endpoint)
    {
        try
        {
            // Objeto.
            var client = new Client(Url);

            Uri.TryCreate(new Uri(Url), endpoint, out Uri? result);

            client.BaseAddress = result;
            return client;
        }
        catch (Exception)
        {
        }

        return new();

    }




    /// <summary>
    /// Establecer la Url.
    /// </summary>
    /// <param name="url">Url default.</param>
    public void SetDefault(string url)
    {
        DefaultUrl = url;
    }



    /// <summary>
    /// Convertir la URL.
    /// </summary>
    /// <param name="url">endpoint</param>
    public string PathURL(string url)
    {
        Uri.TryCreate(new Uri(Url), url, out Uri? result);
        return result?.ToString() ?? "";
    }


}