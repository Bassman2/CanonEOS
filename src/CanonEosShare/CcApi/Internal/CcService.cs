using RestService;

namespace CanonEos.CcApi.Internal;

internal class CcService : JsonService 
{

    public CcService(Uri host) : base(host)
    { }

    //private async Task<T?> GetFromJsonAsync<T>(string? requestUri)
    //{
    //    try
    //    {
    //        HttpResponseMessage response = await this.client.GetAsync(requestUri);
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            ErrorMessage? msg = await response.Content.ReadFromJsonAsync<ErrorMessage>(this.jsonSerializerOptions);
    //            throw new CcException(msg?.Message, response.StatusCode);
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

    #region Finder

    #endregion

    #region HTTP 

    private T? GetFromJson<T>(string? requestUri)
    {
        if (client is null) throw new Exception("Client not connected!");

        using HttpResponseMessage response = client.GetAsync(requestUri).Result;
        string str = response.Content.ReadAsStringAsync().Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(jsonSerializerOptions).Result;
            throw new CcException(msg?.Message, requestUri, response.StatusCode);
        }
        return response.Content.ReadFromJsonAsync<T>(jsonSerializerOptions).Result;
    }

    private T? PutAsJson<T>(string? requestUri, T obj)
    {
        if (client is null) throw new Exception("Client not connected!");

        using HttpResponseMessage response = client.PutAsJsonAsync(requestUri, obj, jsonSerializerOptions).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(jsonSerializerOptions).Result;
            throw new CcException(msg?.Message, requestUri, response.StatusCode);
        }
        return response.Content.ReadFromJsonAsync<T>(jsonSerializerOptions).Result;
    }

    private void PostAsJson(string? requestUri, object obj)
    {
        if (client is null) throw new Exception("Client not connected!");

        using HttpResponseMessage response = client.PostAsJsonAsync(requestUri, obj, jsonSerializerOptions).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(jsonSerializerOptions).Result;
            throw new CcException(msg?.Message, requestUri, response.StatusCode);
        }
    }

    private Stream GetFromStream(string? requestUri)
    {
        if (client is null) throw new Exception("Client not connected!");

        using HttpResponseMessage response = client.GetAsync(requestUri).Result;
        string str = response.Content.ReadAsStringAsync().Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(jsonSerializerOptions).Result;
            throw new CcException(msg?.Message, requestUri, response.StatusCode);
        }
        MemoryStream stream = new MemoryStream();
        response.Content.CopyToAsync(stream).Wait();
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
        //return response.Content.ReadAsStream();
    }

    private void Delete(string? requestUri)
    {
        if (client is null) throw new Exception("Client not connected!");

        using HttpResponseMessage response = client.DeleteAsync(requestUri).Result;
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage? msg = response.Content.ReadFromJsonAsync<ErrorMessage>(jsonSerializerOptions).Result;
            throw new CcException(msg?.Message, requestUri, response.StatusCode);
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

    public DeviceStatusBattery? GetDeviceStatusBattery()
        => GetFromJson<DeviceStatusBattery>("/ccapi/ver110/devicestatus/battery");

    public DeviceStatusBatteries? GetDeviceStatusBatteries()
        => GetFromJson<DeviceStatusBatteries>("/ccapi/ver110/devicestatus/batterylist");

    public Lens? GetDeviceStatusLens()
        => GetFromJson<Lens>("/ccapi/ver100/devicestatus/lens");

    public TemperatureStatus? GetDeviceStatusTemperature()
        => GetFromJson<TempStatus>("/ccapi/ver100/devicestatus/temperature")?.Status;

    #endregion

    #region Camera Settings

    public string? GetCopyright()
        => GetFromJson<CameraCopyright>("/ccapi/ver100/functions/registeredname/copyright")?.Copyright;

    public void SetCopyright(string? value)
        => PutAsJson("/ccapi/ver100/functions/registeredname/copyright", new CameraCopyright() { Copyright = value });

    public string? GetAuthor()
        => GetFromJson<CameraAuthor>("/ccapi/ver100/functions/registeredname/author")?.Author;
    public void SetAuthor(string? value)
        => PutAsJson("/ccapi/ver100/functions/registeredname/author", new CameraAuthor() { Author = value });

    public string? GetOwner()
       => GetFromJson<CameraOwnerName>("/ccapi/ver100/functions/registeredname/ownername")?.OwnerName;

    public void SetOwner(string? value)
        => PutAsJson("/ccapi/ver100/functions/registeredname/ownername", new CameraOwnerName() { OwnerName = value });


    public string? GetNickname()
       => GetFromJson<CameraNickname>("/ccapi/ver100/functions/registeredname/nickname")?.Nickname;

    public void SetNickname(string? value)
        => PutAsJson("/ccapi/ver100/functions/registeredname/nickname", new CameraNickname() { Nickname = value });

    public DateTime? GetDateTime()
       => GetFromJson<CameraDateTime>("/ccapi/ver100/functions/datetime");

    public void SetDateTime(DateTime? value)
       => PutAsJson("/ccapi/ver100/functions/datetime", (CameraDateTime?)value);


    public ValueAbility? GetBeep() => GetFromJson<ValueAbility>("/ccapi/ver100/functions/beep");

    public void SetBeep(string value) => PutAsJson("/ccapi/ver100/functions/beep", new ValueAbility() { Value = value });

    public ValueAbility? GetDisplayOff() => GetFromJson<ValueAbility>("/ccapi/ver100/functions/displayoff");

    public void SetDisplayOff(string value) => PutAsJson("/ccapi/ver100/functions/displayoff", new ValueAbility() { Value = value });

    public ValueAbility? GetAutoPowerOff() => GetFromJson<ValueAbility>("/ccapi/ver100/functions/autopoweroff");

    public void SetAutoPowerOff(string? value) => PutAsJson("/ccapi/ver100/functions/autopoweroff", new ValueAbility() { Value = value });

    public void Format(string card)
       => PostAsJson("/ccapi/ver100/functions/cardformat", new StorageName() { Name = card });

    //public ValueGet? GetMute()
    //   => GetFromJson<ValueGet>("/ccapi/ver100/functions/beep");

    //public ValuePut? SetMute(string value)
    //   => PutAsJson<ValuePut>("/ccapi/ver100/functions/beep", new ValuePut { Value = value });

    #endregion

    #region Image Operations

    public IEnumerable<string>? GetVolumns()
        => GetFromJson<PathList>("/ccapi/ver120/contents")?.Paths;

    public IEnumerable<string>? GetDirectories(string volumeName)
        => GetFromJson<PathList>($"/ccapi/ver120/contents/{volumeName}")?.Paths;

    public bool HasFiles(string volumeName, string directoryName)
    {
        return GetFromJson<Number>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=number")?.ContentsNumber > 0;
    }
    
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
