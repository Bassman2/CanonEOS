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

    public override string Name { get; }
    public override string? ProductName { get; }
    public override string? OwnerName { get; }
    public override string? FirmwareVersion { get; }
    public override string? CurrentStorage { get; }
    public override string? CurrentFolder { get; }
    public override string? BodyIDEx { get; }
    public override string? LensName 
    { 
        get
        {
            return this.service.GetDeviceStatusLens()?.Name;
        }
    }
    public override string? Artist { get; }
    public override string? Copyright { get; }

    public override IEnumerable<Volume> Volumes
    {
        get
        {
            var volumes = this.service.GetDeviceStatusStorage110();
            return volumes?.Storages?.Select(s => new CcVolume(this.service, s)) ?? [];
        }
    }
    public override IEnumerable<Property> Properties { get; }
}
