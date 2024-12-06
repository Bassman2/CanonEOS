
namespace CanonEos.CcApi;

public sealed class CcCamera : Camera
{
    private CcService? service;
    private CameraDevDesc? cameraDevDesc;
    private DeviceInformation? deviceInformation;

    public CcCamera()
    { }

    internal CcCamera(CameraDevDesc devDesc)
    {
        _ = Open(devDesc) ? 0 : throw new CanonException($"Failed to open {devDesc?.Device?.ServiceList?[0].AccessURL}");
    }

    public void Dispose() => Close();
    
    public bool IsOpen => this.service is not null;

    public bool Open(string host)
    {
        _ = CcService.PingCamera(host) ? 0 : throw new CanonException(host, "No connected device");

        CameraDevDesc? cameraDevDesc = CcService.GetCameraDevDescAsync(host, default).Result;
        if (cameraDevDesc == null) throw new CanonException(host, "No connected Canon device");

        return Open(cameraDevDesc);
    }

    public bool Open(CameraDevDesc devDesc)
    {
        this.cameraDevDesc = devDesc;

        var uri = new Uri(devDesc.Device!.ServiceList![0].AccessURL!);

        this.service = new CcService(uri);
        //if (this.service.Connect(uri) ==  false)
        //{
        //    return false;
        //}

        this.deviceInformation = this.service.GetDeviceInformationAsync(default).Result;
        return true;
    }

    public void Close()
    {
        if (this.service is not null)
        {
            this.service.Dispose();
            this.service = null;
        }
    }


    #region information

    public ConnectionType ConnectionType => ConnectionType.WiFi;
    public string? Name  => cameraDevDesc?.Device?.ServiceList?[0].DeviceNickname;
    public string? ProductName => deviceInformation?.ProductName; 
    public string? FirmwareVersion => deviceInformation?.FirmwareVersion;
    public string? BodyIDEx => deviceInformation?.SerialNumber;
    public string? LensName => service?.GetDeviceStatusLensAsync(default).Result?.Name;
    public string? CurrentStorage => service?.GetDeviceStatusCurrentStorageAsync(default).Result?.Name;
    public string? CurrentFolder => service?.GetDeviceStatusCurrentDirectoryAsync(default).Result?.Name;
    public TemperatureStatus? TemperatureStatus => service?.GetDeviceStatusTemperatureAsync(default).Result;
    public IEnumerable<BatteryInfo>? Batteries => service?.GetDeviceStatusBatteriesAsync(default).Result?.Batteries?.Select(b => new BatteryInfo(b));

    private List<EosVolume>? volumes;
    public IEnumerable<EosVolume>? Volumes => volumes ??= service?.GetDeviceStatusStorageAsync(default).Result?.Select(s => (EosVolume)new CcVolume(this.service, s)).ToList();

    public IEnumerable<Property> Properties { get; } = new Property[0];

    #endregion

    #region settings

    public string? Copyright
    {
        get => service?.GetCopyrightAsync(default).Result;
        set => service?.SetCopyrightAsync(value, default).Wait();
    }

    public string? Author
    {
        get => service?.GetAuthorAsync(default).Result;
        set => service?.SetAuthorAsync(value, default).Wait();
    }

    public string? Owner
    {
        get => service?.GetOwnerAsync(default).Result;
        set => service?.SetOwnerAsync(value, default).Wait();
    }

    public string? Nickname
    {
        get => service?.GetNicknameAsync(default).Result;
        set => service?.SetNicknameAsync(value, default).Wait();
    }

    public DateTime? DateTime 
    { 
        get => service?.GetDateTimeAsync(default).Result; 
        set => service?.SetDateTimeAsync(value, default).Wait();
    }

    public string? Beep
    {
        get => service?.GetBeepAsync(default).Result.Ability(out beepValues);
        set => service?.SetBeepAsync(value!, default).Wait();
    }

    private string[]? beepValues;
    public string[]? BeepValues => beepValues ??= service?.GetBeepAsync(default).Result?.Ability?.ToArray();
    
    public string? DisplayOff
    {
        get => service?.GetDisplayOffAsync(default).Result.Ability(out displayOffValues);
        set => service?.SetDisplayOffAsync(value!, default).Wait();
    }

    private string[]? displayOffValues;
    public string[]? DisplayOffValues => displayOffValues ??= service?.GetDisplayOffAsync(default).Result?.Ability?.ToArray();

    public string? AutoPowerOff
    {
        get => service?.GetAutoPowerOffAsync(default).Result.Ability(out autoPowerOffValues);
        set => service?.SetAutoPowerOffAsync(value, default).Wait();
    }

    private string[]? autoPowerOffValues;
    public string[]? AutoPowerOffValues => autoPowerOffValues ??= service?.GetAutoPowerOffAsync(default).Result?.Ability?.ToArray();

    #endregion
}
