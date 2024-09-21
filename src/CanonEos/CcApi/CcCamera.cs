namespace CanonEos.CcApi;

internal class CcCamera : Camera
{
    private CcService? service;
    private DeviceInformation? deviceInformation;

    public CcCamera()
    { }

    public bool Connect(string host)
    {
        this.service = new CcService();
        if (!this.service.Connect(host))
        {
            return false;
        }
        this.deviceInformation = this.service.GetDeviceInformation();

        //this.Name = deviceInformation?.ProductName ?? url.OriginalString;
        //this.ProductName = deviceInformation?.ProductName ;
        //this.FirmwareVersion = deviceInformation?.FirmwareVersion;
        //this.BodyIDEx = deviceInformation?.SerialNumber;

        return true;
    }

    public override void Dispose()
    { }


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


    public override IEnumerable<BatteryInfo>? Batteries =>
        service?.GetDeviceStatusBatteries()?.Batteries?.Select(b => new BatteryInfo(b));

    private List<Volume>? volumes;
    public override IEnumerable<Volume>? Volumes 
        => volumes ??= service?.GetDeviceStatusStorage()?.Select(s => (Volume)new CcVolume(this.service, s)).ToList();

    public override IEnumerable<Property> Properties { get; } = [];
}
