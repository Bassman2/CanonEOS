namespace CanonEos.CcApi;

internal class CcService : IDisposable
{
    private HttpClientHandler? handler;
    private HttpClient? client;
    //private readonly string sessionId;
    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
        }
    };

    public CcService() { }

    public bool Connect(string host)
    {
        bool success = PingCamera(host);
        if (!success) return false;

        CameraDevDesc? cameraDevDesc = GetCameraDevDesc(host);
        if (cameraDevDesc == null) return false;

        // connect
        this.handler = new HttpClientHandler
        {
            CookieContainer = new System.Net.CookieContainer(),
            UseCookies = true,
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
        };

        this.client = new HttpClient(this.handler)
        {
            BaseAddress = new Uri(cameraDevDesc!.Device!.ServiceList![0].AccessURL!),
            Timeout = new TimeSpan(0, 2, 0)
        };


        Ccapis? apis = GetApiList();


        //Ccapis? apisV100 = GetApiList("ver100");
        //Ccapis? apisV110 = GetApiList("ver110");
        //Ccapis? apisV120 = GetApiList("ver120");
        //Ccapis? apisV130 = GetApiList("ver130");
        //Ccapis? apisV140 = GetApiList("ver140");
        return true;

    }

    private bool OnServerCertificateValidation(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;

    }

    public void Dispose()
    {
        if (this.client is not null)
        {
            this.client.Dispose();
            this.client = null;
        }
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


    public static bool PingCamera(Uri url) => PingCamera(url.Host);

    public static bool PingCamera(string host)
    {
        PingReply rep = new Ping().SendPingAsync(host, 1000).Result;
        return rep.Status == IPStatus.Success;
    }


    public static CameraDevDesc? GetCameraDevDesc(Uri url) => GetCameraDevDesc(url.Host);

    public static CameraDevDesc? GetCameraDevDesc(string host)
    {
        Uri upnpUri = new UriBuilder("http", host, 49152, "/upnp/CameraDevDesc.xml").Uri;
        using HttpClient upnp = new HttpClient();

        string text = upnp.GetStringAsync(upnpUri).Result;

        XmlSerializer serializer = new XmlSerializer(typeof(CameraDevDesc));
        CameraDevDesc? cameraDevDesc = (CameraDevDesc?)serializer.Deserialize(new StringReader(text));
        return cameraDevDesc;

    }

    #region HTTP 

    private T? GetFromJson<T>(string? requestUri)
    {
        using HttpResponseMessage response = this.client.GetAsync(requestUri).Result;
        string str = response.Content.ReadAsStringAsync().Result;
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

    private Stream GetFromStream(string? requestUri)
    {
        using HttpResponseMessage response = this.client.GetAsync(requestUri).Result;
        string str = response.Content.ReadAsStringAsync().Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions).Result;
            throw new CCException(msg?.Message, response.StatusCode);
        }
        MemoryStream stream = new MemoryStream();
        response.Content.CopyToAsync(stream).Wait();
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
        //return response.Content.ReadAsStream();
    }

    private void Delete(string? requestUri)
    {
        using HttpResponseMessage response = this.client.DeleteAsync(requestUri).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions).Result;
            throw new CCException(msg?.Message, response.StatusCode);
        }
    }

    #endregion

    #region List of APIs

    public Ccapis? GetApiList()
        => GetFromJson<Ccapis>("/ccapi");

    public Ccapis? GetApiList(string version)
       => GetFromJson<Ccapis>($"/ccapi/{version}/topurlfordev");

    #endregion

    #region Camera Information (Fixed Values)

    public DeviceInformation? GetDeviceInformation() 
        => GetFromJson<DeviceInformation>("/ccapi/ver100/deviceinformation");

    #endregion

    #region Camera Status (Variable Values)
        
    public IEnumerable<Storage>? GetDeviceStatusStorage()
        => GetFromJson<DeviceStatusStorage>("/ccapi/ver110/devicestatus/storage")?.Storages;

    public DeviceStatusCurrentStorage? GetDeviceStatusCurrentStorage()
        => GetFromJson<DeviceStatusCurrentStorage>("/ccapi/ver110/devicestatus/currentstorage");

    public DeviceStatusCurrentDirectory? GetDeviceStatusCurrentDirectory()
        => GetFromJson<DeviceStatusCurrentDirectory>("/ccapi/ver110/devicestatus/currentdirectory");

    public Battery? GetDeviceStatusBattery()
        => GetFromJson<Battery>("/ccapi/ver110/devicestatus/battery");

    public Batteries? GetDeviceStatusBatteries()
        => GetFromJson<Batteries>("/ccapi/ver110/devicestatus/batterylist");

    public Lens? GetDeviceStatusLens()
        => GetFromJson<Lens>("/ccapi/ver100/devicestatus/lens");

    public Temperature? GetDeviceStatusTemerature()
        => GetFromJson<Temperature>("/ccapi/ver100/devicestatus/temperature");

    #endregion

    #region Camera Settings

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

    #endregion

    #region Image Operations

    public IEnumerable<string>? GetVolumns()
        => GetFromJson<PathList>("/ccapi/ver120/contents")?.Paths;

    public IEnumerable<string>? GetDirectories(string volumeName)
        => GetFromJson<PathList>($"/ccapi/ver120/contents/{volumeName}")?.Paths;

    public IEnumerable<string>? GetFiles(string volumeName, string directoryName)
    {
        Number? number = GetFromJson<Number>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=number");
        for (uint page = 1; page <= number!.PageNumber; page++)
        {
            var list = GetFromJson<PathList>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=list&page={page}")?.Paths;
            foreach (var path in list!)
            {
                yield return path;
            }
        }
    }

    public void DeleteDirectory(string volumeName, string directoryName)
        => Delete($"/ccapi/ver130/contents/{volumeName}/{directoryName}");

    public Stream? DownloadImage(string volumeName, string directoryName, string fileName)
    => GetFromStream($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=main");

    public Stream? DownloadThumbnail(string volumeName, string directoryName, string fileName)
    => GetFromStream($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=thumbnail");

    public Stream? DownloadDisplay(string volumeName, string directoryName, string fileName)
    => GetFromStream($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=display");

    public Stream? DownloadEmbedded(string volumeName, string directoryName, string fileName)
    => GetFromStream($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=embedded");

    public ImageInfo? GetFileInfo(string volumeName, string directoryName, string fileName)
        => GetFromJson<ImageInfo>($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=info");

    #endregion

    #region Shooting Control

    #endregion
}
