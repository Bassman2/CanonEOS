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

        CameraDevDesc? cameraDevDesc = CcService.GetCameraDevDesc(host);
        if (cameraDevDesc == null) throw new CanonException(host, "No connected Canon device");

        return Open(cameraDevDesc);
    }

    public bool Open(CameraDevDesc devDesc)
    {
        this.cameraDevDesc = devDesc;

        var uri = new Uri(devDesc.Device!.ServiceList![0].AccessURL!);

        this.service = new CcService();
        if (this.service.Connect(uri) ==  false)
        {
            return false;
        }

        this.deviceInformation = this.service.GetDeviceInformation();
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
    public string? LensName => service?.GetDeviceStatusLens()?.Name;
    public string? CurrentStorage => service?.GetDeviceStatusCurrentStorage()?.Name;
    public string? CurrentFolder => service?.GetDeviceStatusCurrentDirectory()?.Name;
    public TemperatureStatus? TemperatureStatus => service?.GetDeviceStatusTemperature();
    public IEnumerable<BatteryInfo>? Batteries => service?.GetDeviceStatusBatteries()?.Batteries?.Select(b => new BatteryInfo(b));

    private List<EosVolume>? volumes;
    public IEnumerable<EosVolume>? Volumes => volumes ??= service?.GetDeviceStatusStorage()?.Select(s => (EosVolume)new CcVolume(this.service, s)).ToList();

    public IEnumerable<Property> Properties { get; } = [];

    #endregion

    #region settings

    public string? Copyright
    {
        get => service?.GetCopyright();
        set => service?.SetCopyright(value);
    }

    public string? Author
    {
        get => service?.GetAuthor();
        set => service?.SetAuthor(value);
    }

    public string? Owner
    {
        get => service?.GetOwner();
        set => service?.SetOwner(value);
    }

    public string? Nickname
    {
        get => service?.GetNickname();
        set => service?.SetNickname(value);
    }

    public DateTime? DateTime 
    { 
        get => service?.GetDateTime(); 
        set => service?.SetDateTime(value);
    }

    public string? Beep
    {
        get => service?.GetBeep().Ability(out beepValues);
        set => service?.SetBeep(value!);
    }

    private string[]? beepValues;
    public string[]? BeepValues => beepValues ??= service?.GetBeep()?.Ability?.ToArray();
    
    public string? DisplayOff
    {
        get => service?.GetDisplayOff().Ability(out displayOffValues);
        set => service?.SetDisplayOff(value!);
    }

    private string[]? displayOffValues;
    public string[]? DisplayOffValues => displayOffValues ??= service?.GetDisplayOff()?.Ability?.ToArray();

    public string? AutoPowerOff
    {
        get => service?.GetAutoPowerOff().Ability(out autoPowerOffValues);
        set => service?.SetAutoPowerOff(value);
    }

    private string[]? autoPowerOffValues;
    public string[]? AutoPowerOffValues => autoPowerOffValues ??= service?.GetAutoPowerOff()?.Ability?.ToArray();

    #endregion
}
