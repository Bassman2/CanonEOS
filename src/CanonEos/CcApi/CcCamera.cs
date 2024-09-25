namespace CanonEos.CcApi;

public sealed class CcCamera : Camera
{
    private CcService? service;
    private DeviceInformation? deviceInformation;

    internal CcCamera(CameraDevDesc devDesc)
    {
        var uri = new Uri(devDesc.Device!.ServiceList![0].AccessURL!);
        
        this.service = new CcService();
        this.service.Connect(uri);
        
        this.deviceInformation = this.service.GetDeviceInformation();
    }

    public override void Dispose()
    {
        if (this.service is not null)
        {
            this.service.Dispose(); 
            this.service = null;
        }
    }

    public override ConnectionType Type => ConnectionType.WiFi;

    // Device Information
    public override string? Name => deviceInformation?.ProductName;
    public override string? ProductName => deviceInformation?.ProductName; 
    public override string? FirmwareVersion => deviceInformation?.FirmwareVersion;
    public override string? BodyIDEx => deviceInformation?.SerialNumber;

    // Requests
    public override string? CurrentStorage => service?.GetDeviceStatusCurrentStorage()?.Name;
    public override string? CurrentFolder => service?.GetDeviceStatusCurrentDirectory()?.Name;
    
    public override string? LensName => service?.GetDeviceStatusLens()?.Name;

    // settings
    public override string? Copyright
    {
        get => service?.GetCopyright();
        set => service?.SetCopyright(value);
    }

    public override string? Artist
    {
        get => service?.GetAuthor();
        set => service?.SetAuthor(value);
    }

    public override string? OwnerName
    {
        get => service?.GetOwnerName();
        set => service?.SetOwnerName(value);
    }

    public override DateTime? DateTime 
    { 
        get => service?.GetDateTime(); 
        set => service?.SetDateTime(value);
    }



    public override IEnumerable<BatteryInfo>? Batteries =>
        service?.GetDeviceStatusBatteries()?.Batteries?.Select(b => new BatteryInfo(b));

    private List<Volume>? volumes;
    public override IEnumerable<Volume>? Volumes 
        => volumes ??= service?.GetDeviceStatusStorage()?.Select(s => (Volume)new CcVolume(this.service, s)).ToList();

    public override IEnumerable<Property> Properties { get; } = [];
}
