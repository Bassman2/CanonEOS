
using System.Xml.Linq;

namespace CanonEos.CcApi;

internal class CcService : IDisposable
{
    private readonly Uri defaultHost = new Uri("http://fritz.box");
    private readonly HttpClientHandler? handler;
    private HttpClient client;
    //private readonly string sessionId;
    private JsonSerializerOptions? jsonSerializerOptions = null;

    public CcService(Uri url)
    {
        this.jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
            }
        };

        //ServicePointManager.ServerCertificateValidationCallback += OnServerCertificateValidation;
        //    //(sender, cert, chain, sslPolicyErrors) => true;

        // connect
        this.handler = new HttpClientHandler
        {
            CookieContainer = new System.Net.CookieContainer(),
            UseCookies = true,
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
        };
        
        //handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        //handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;
        
        this.client = new HttpClient(this.handler)
        {
            BaseAddress = url,
            Timeout = new TimeSpan(0, 2, 0)
        };

        string res = this.client.GetStringAsync("ccapi").Result;

        DeviceInformation? deviceInformation = GetDeviceInformation();

        DeviceStatusStorage110? deviceStatusStorage = GetDeviceStatusStorage110();

        DeviceStatusCurrentStorage110? deviceStatusCurrentStorage = GetDeviceStatusCurrentStorage110();

    }

    private bool OnServerCertificateValidation(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;

    }

    public void Dispose()
    {
        this.client.Dispose();
    }

    //private async Task<T?> GetFromJsonAsync<T>(string? requestUri)
    //{
    //    try
    //    {
    //        HttpResponseMessage response = await this.client.GetAsync(requestUri);
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            ErrorMessage? msg = await response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions);
    //            throw new CCException(msg?.Message, response.StatusCode);
    //        }
    //        return await response.Content.ReadFromJsonAsync<T>(this.jsonSerializerOptions);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    private T? GetFromJson<T>(string? requestUri)
    {
        using HttpResponseMessage response = this.client.GetAsync(requestUri).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions).Result;
            throw new CCException(msg?.Message, response.StatusCode);
        }
        return response.Content.ReadFromJsonAsync<T>(this.jsonSerializerOptions).Result;
    }

    private T? PutAsJson<T>(string? requestUri, T obj)
    {
        using HttpResponseMessage response = this.client.PutAsJsonAsync(requestUri, obj, this.jsonSerializerOptions).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions).Result;
            throw new CCException(msg?.Message, response.StatusCode);
        }
        return response.Content.ReadFromJsonAsync<T>(this.jsonSerializerOptions).Result;
    }

    private void PostAsJson(string? requestUri, object obj)
    {
        using HttpResponseMessage response = this.client.PostAsJsonAsync(requestUri, obj, this.jsonSerializerOptions).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions).Result;
            throw new CCException(msg?.Message, response.StatusCode);
        }
    }

    public DeviceInformation? GetDeviceInformation() 
        => GetFromJson<DeviceInformation>("/ccapi/ver100/deviceinformation");

    public DeviceStatusStorage100? GetDeviceStatusStorage100()
        => GetFromJson<DeviceStatusStorage100>("/ccapi/ver100/devicestatus/storage");

    public DeviceStatusStorage110? GetDeviceStatusStorage110()
        => GetFromJson<DeviceStatusStorage110>("/ccapi/ver110/devicestatus/storage");

    public DeviceStatusCurrentStorage110? GetDeviceStatusCurrentStorage110()
        => GetFromJson<DeviceStatusCurrentStorage110>("/ccapi/ver110/devicestatus/currentstorage");

    public Battery? GetDeviceStatusBattery()
        => GetFromJson<Battery>("/ccapi/ver110/devicestatus/battery");

    public Batteries? GetDeviceStatusBatteries()
        => GetFromJson<Batteries>("/ccapi/ver110/devicestatus/currentstorage");

    public Lens? GetDeviceStatusLens()
        => GetFromJson<Lens>("/ccapi/ver100/devicestatus/lens");

    public Temperature? GetDeviceStatusTemerature()
        => GetFromJson<Temperature>("/ccapi/ver100/devicestatus/temperature");

    public string? GetCopyright()
        => GetFromJson<CameraCopyright>("/ccapi/ver100/functions/registeredname/copyright")?.Copyright;

    public string? GetAuthor()
        => GetFromJson<CameraAuthor>("/ccapi/ver100/functions/registeredname/author")?.Author;

    public string? GetOwnerName()
       => GetFromJson<CameraOwnerName>("/ccapi/ver100/functions/registeredname/ownername")?.OwnerName;

    public string? GetNickname()
       => GetFromJson<CameraNickname>("/ccapi/ver100/functions/registeredname/nickname")?.Nickname;

    public CameraDateTime? GetDateTime()
       => GetFromJson<CameraDateTime>("/ccapi/ver100/functions/datetime");


    public void Format(string card)
       => PostAsJson("/ccapi/ver100/functions/cardformat", new StorageName() { Name = card });

    public ValueGet? GetMute()
       => GetFromJson<ValueGet>("/ccapi/ver100/functions/beep");

    public ValuePut? SetMute(string value)
       => PutAsJson<ValuePut>("/ccapi/ver100/functions/beep", new ValuePut { Value = value });
}
