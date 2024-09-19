using System.Net;

namespace CanonEos.CcApi;

internal class CcCamera : Camera
{
    private CcService service;

    internal CcCamera(Uri url)
    {
        this.service = new CcService(url);

      

        DeviceInformation? deviceInformation = this.service.GetDeviceInformation();

        this.Name = deviceInformation?.ProductName ?? url.OriginalString;
        this.ProductName = deviceInformation?.ProductName ;
        this.FirmwareVersion = deviceInformation?.FirmwareVersion;
        this.BodyIDEx = deviceInformation?.SerialNumber;

        this.Properties = [];
    }

    public override void Dispose()
    { }


    // Device Information
    public override string Name { get; }
    public override string? ProductName { get; }
    public override string? FirmwareVersion { get; }
    public override string? BodyIDEx { get; }

    // Requests
    public override string? CurrentStorage => service.GetDeviceStatusCurrentStorage()?.Name;
    public override string? CurrentFolder => service.GetDeviceStatusCurrentDirectory()?.Name;
    
    public override string? LensName => this.service.GetDeviceStatusLens()?.Name;
    public override string? OwnerName => service.GetOwnerName();
    public override string? Artist => service.GetAuthor();
    public override string? Copyright => service.GetCopyright();

    
    private List<Volume>? volumes;
    public override IEnumerable<Volume>? Volumes 
        => volumes ??= service.GetDeviceStatusStorage()?.Select(s => (Volume)new CcVolume(this.service, s)).ToList();
        
    public override IEnumerable<Property> Properties { get; }
}
