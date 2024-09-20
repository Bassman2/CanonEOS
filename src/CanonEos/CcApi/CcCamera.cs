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
    public override string? OwnerName => service?.GetOwnerName();
    public override string? Artist => service?.GetAuthor();
    public override string? Copyright => service?.GetCopyright();

    public override IEnumerable<BatteryInfo>? Batteries =>
        service?.GetDeviceStatusBatteries()?.BatteryList?.Select(b => new BatteryInfo()
        {
             Position = b.Position,
             Name = b.Name,
             Kind = b.Kind,
             Level = b.Level,
             Quality = b.Quality
        });


    private List<Volume>? volumes;
    public override IEnumerable<Volume>? Volumes 
        => volumes ??= service?.GetDeviceStatusStorage()?.Select(s => (Volume)new CcVolume(this.service, s)).ToList();

    public override IEnumerable<Property> Properties { get; } = [];
}
