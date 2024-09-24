namespace CanonEos;

public abstract class Camera : IDisposable
{
    public abstract void Dispose();
    public abstract ConnectionType Type { get; }
    
    public abstract string? Name { get; }
    public abstract string? ProductName { get; }
    public abstract string? FirmwareVersion { get; }
    public abstract string? CurrentStorage { get; }
    public abstract string? CurrentFolder { get; }
    public abstract string? BodyIDEx { get; }
    public abstract string? LensName { get; }
    
    // settings 
    public abstract string? Copyright { get; set; }
    public abstract string? Artist { get; set; }
    public abstract string? OwnerName { get; set; }
    public abstract DateTime? DateTime { get; set; }

    public abstract IEnumerable<BatteryInfo>? Batteries { get; }

    public abstract IEnumerable<Volume>? Volumes { get; }

    public abstract IEnumerable<Property> Properties { get; }

}
