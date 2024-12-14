namespace CanonEos.CcApi.Internal;

internal class CcService(Uri host) : JsonService(host, SourceGenerationContext.Default)
{
    public static bool PingCamera(Uri url) => PingCamera(url.Host);

    public static bool PingCamera(string host)
    {
        PingReply rep = new Ping().SendPingAsync(host, 1000).Result;
        return rep.Status == IPStatus.Success;
    }

    protected override void TestAutentication()
    {
        //TODO
    }

    public static async Task<CameraDevDesc?> GetCameraDevDescAsync(Uri url, CancellationToken cancellationToken) => await GetCameraDevDescAsync(url.Host, cancellationToken);

    public static async Task<CameraDevDesc?> GetCameraDevDescAsync(string host, CancellationToken cancellationToken)
    {
        Uri upnpUri = new UriBuilder("http", host, 49152, "/upnp/CameraDevDesc.xml").Uri;
        using HttpClient upnp = new HttpClient();

        string text = await upnp.GetStringAsync(upnpUri);

        //var serializer = new XmlSerializer(typeof(CameraDevDesc));
        //CameraDevDesc? cameraDevDesc = (CameraDevDesc?)serializer.Deserialize(new StringReader(text));

        var cameraDevDesc = text.XDeserialize<CameraDevDesc>("root");   // Namespace="urn:schemas-upnp-org:device-1-0"
        return cameraDevDesc;

    }

    #region Finder

    #endregion

    #region List of APIs

    public async Task<Ccapis?> GetApiListAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<Ccapis>("/ccapi", cancellationToken);

    public async Task<Ccapis?> GetApiListAsync(string version, CancellationToken cancellationToken)
       => await GetFromJsonAsync<Ccapis>($"/ccapi/{version}/topurlfordev", cancellationToken);

    #endregion

    #region Camera Information (Fixed Values)

    public async Task<DeviceInformation?> GetDeviceInformationAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<DeviceInformation>("/ccapi/ver100/deviceinformation", cancellationToken);

    #endregion

    #region Camera Status (Variable Values)

    public async Task<IEnumerable<Storage>?> GetDeviceStatusStorageAsync(CancellationToken cancellationToken)
        => (await GetFromJsonAsync<DeviceStatusStorage>("/ccapi/ver110/devicestatus/storage", cancellationToken))?.Storages;

    public async Task<DeviceStatusCurrentStorage?> GetDeviceStatusCurrentStorageAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<DeviceStatusCurrentStorage>("/ccapi/ver110/devicestatus/currentstorage", cancellationToken);

    public async Task<DeviceStatusCurrentDirectory?> GetDeviceStatusCurrentDirectoryAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<DeviceStatusCurrentDirectory>("/ccapi/ver110/devicestatus/currentdirectory", cancellationToken);

    public async Task<DeviceStatusBattery?> GetDeviceStatusBatteryAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<DeviceStatusBattery>("/ccapi/ver110/devicestatus/battery", cancellationToken);

    public async Task<DeviceStatusBatteries?> GetDeviceStatusBatteriesAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<DeviceStatusBatteries>("/ccapi/ver110/devicestatus/batterylist", cancellationToken);

    public async Task<Lens?> GetDeviceStatusLensAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<Lens>("/ccapi/ver100/devicestatus/lens", cancellationToken);

    public async Task<TemperatureStatus?> GetDeviceStatusTemperatureAsync(CancellationToken cancellationToken)
        => (await GetFromJsonAsync<TempStatus>("/ccapi/ver100/devicestatus/temperature", cancellationToken))?.Status;

    #endregion

    #region Camera Settings

    public async Task<string?> GetCopyrightAsync(CancellationToken cancellationToken)
        => (await GetFromJsonAsync<CameraCopyright>("/ccapi/ver100/functions/registeredname/copyright", cancellationToken))?.Copyright;

    public async Task SetCopyrightAsync(string? value, CancellationToken cancellationToken)
        => await PutAsJsonAsync("/ccapi/ver100/functions/registeredname/copyright", new CameraCopyright() { Copyright = value }, cancellationToken);

    public async Task<string?> GetAuthorAsync(CancellationToken cancellationToken)
        => (await GetFromJsonAsync<CameraAuthor>("/ccapi/ver100/functions/registeredname/author", cancellationToken))?.Author;

    public async Task SetAuthorAsync(string? value, CancellationToken cancellationToken)
        => await PutAsJsonAsync("/ccapi/ver100/functions/registeredname/author", new CameraAuthor() { Author = value }, cancellationToken);

    public async Task<string?> GetOwnerAsync(CancellationToken cancellationToken)
       => (await GetFromJsonAsync<CameraOwnerName>("/ccapi/ver100/functions/registeredname/ownername", cancellationToken))?.OwnerName;

    public async Task SetOwnerAsync(string? value, CancellationToken cancellationToken)
        => await PutAsJsonAsync("/ccapi/ver100/functions/registeredname/ownername", new CameraOwnerName() { OwnerName = value }, cancellationToken);


    public async Task<string?> GetNicknameAsync(CancellationToken cancellationToken)
       => (await GetFromJsonAsync<CameraNickname>("/ccapi/ver100/functions/registeredname/nickname", cancellationToken))?.Nickname;

    public async Task SetNicknameAsync(string? value, CancellationToken cancellationToken)
        => await PutAsJsonAsync("/ccapi/ver100/functions/registeredname/nickname", new CameraNickname() { Nickname = value }, cancellationToken);

    public async Task<DateTime?> GetDateTimeAsync(CancellationToken cancellationToken)
       => (await GetFromJsonAsync<CameraDateTime>("/ccapi/ver100/functions/datetime", cancellationToken))?.DateTime;

    public async Task SetDateTimeAsync(DateTime? value, CancellationToken cancellationToken)
       => await PutAsJsonAsync("/ccapi/ver100/functions/datetime", (CameraDateTime?)value, cancellationToken);


    public async Task<ValueAbility?> GetBeepAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<ValueAbility>("/ccapi/ver100/functions/beep", cancellationToken);

    public async Task SetBeepAsync(string value, CancellationToken cancellationToken) 
        => await PutAsJsonAsync("/ccapi/ver100/functions/beep", new ValueAbility() { Value = value }, cancellationToken);

    public async Task<ValueAbility?> GetDisplayOffAsync(CancellationToken cancellationToken)
        => await GetFromJsonAsync<ValueAbility>("/ccapi/ver100/functions/displayoff", cancellationToken);

    public async Task SetDisplayOffAsync(string value, CancellationToken cancellationToken)
        => await PutAsJsonAsync("/ccapi/ver100/functions/displayoff", new ValueAbility() { Value = value }, cancellationToken);

    public async Task<ValueAbility?> GetAutoPowerOffAsync(CancellationToken cancellationToken) 
        => await GetFromJsonAsync<ValueAbility>("/ccapi/ver100/functions/autopoweroff", cancellationToken);

    public async Task SetAutoPowerOffAsync(string? value, CancellationToken cancellationToken) 
        => await PutAsJsonAsync("/ccapi/ver100/functions/autopoweroff", new ValueAbility() { Value = value }, cancellationToken);

    public async Task FormatAsync(string card, CancellationToken cancellationToken)
       => await PostAsJsonAsync("/ccapi/ver100/functions/cardformat", new StorageName() { Name = card }, cancellationToken);

    //public ValueGet? GetMute()
    //   => GetFromJson<ValueGet>("/ccapi/ver100/functions/beep");

    //public ValuePut? SetMute(string value)
    //   => PutAsJson<ValuePut>("/ccapi/ver100/functions/beep", new ValuePut { Value = value });

    #endregion

    #region Image Operations

    public async Task<IEnumerable<string>?> GetVolumnsAsync(CancellationToken cancellationToken)
        => (await GetFromJsonAsync<PathList>("/ccapi/ver120/contents", cancellationToken))?.Paths;

    public async Task<IEnumerable<string>?> GetDirectoriesAsync(string volumeName, CancellationToken cancellationToken)
        => (await GetFromJsonAsync<PathList>($"/ccapi/ver120/contents/{volumeName}", cancellationToken))?.Paths;

    public async Task<bool> HasFiles(string volumeName, string directoryName, CancellationToken cancellationToken)
    {
        return (await GetFromJsonAsync<Number>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=number", cancellationToken))?.ContentsNumber > 0;
    }
    
    public async IAsyncEnumerable<string> GetFilesAsync(string volumeName, string directoryName, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        Number? number = await GetFromJsonAsync<Number>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=number", cancellationToken);
        for (uint page = 1; page <= number!.PageNumber; page++)
        {
            var list = (await GetFromJsonAsync<PathList>($"/ccapi/ver120/contents/{volumeName}/{directoryName}?type=all&kind=list&page={page}", cancellationToken))?.Paths;
            foreach (var path in list!)
            {
                yield return path;
            }
        }
    }

    public async Task DeleteDirectory(string volumeName, string directoryName, CancellationToken cancellationToken)
        => await DeleteAsync($"/ccapi/ver130/contents/{volumeName}/{directoryName}", cancellationToken);

    public async Task<Stream?> DownloadImageAsync(string volumeName, string directoryName, string fileName, CancellationToken cancellationToken)
       => await GetFromStreamAsync($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=main", cancellationToken);

    public async Task<Stream?> DownloadThumbnailAsync(string volumeName, string directoryName, string fileName, CancellationToken cancellationToken)
        => await GetFromStreamAsync($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=thumbnail", cancellationToken);

    public async Task<Stream?> DownloadDisplayAsync(string volumeName, string directoryName, string fileName, CancellationToken cancellationToken)
        => await GetFromStreamAsync($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=display", cancellationToken);

    public async Task<Stream?> DownloadEmbeddedAsync(string volumeName, string directoryName, string fileName, CancellationToken cancellationToken)
        => await GetFromStreamAsync($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=embedded", cancellationToken);

    public async Task<ImageInfo?> GetFileInfoAsync(string volumeName, string directoryName, string fileName, CancellationToken cancellationToken)
        => await GetFromJsonAsync<ImageInfo>($"/ccapi/ver130/contents/{volumeName}/{directoryName}/{fileName}?kind=info", cancellationToken);

    #endregion

    #region Shooting Control

    #endregion
}
