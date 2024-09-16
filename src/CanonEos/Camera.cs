namespace CanonEos;

public abstract class Camera : IDisposable
{
    public abstract void Dispose();

    public abstract string Name { get; }
    public abstract string? ProductName { get; }
    public abstract string? OwnerName { get; }
    public abstract string? FirmwareVersion { get; }
    public abstract string? CurrentStorage { get; }
    public abstract string? CurrentFolder { get; }
    public abstract string? BodyIDEx { get; }
    public abstract string? LensName { get; }
    public abstract string? Artist { get; }
    public abstract string? Copyright { get; }

    public abstract IEnumerable<Volume> Volumes { get; }

    public abstract IEnumerable<Property> Properties { get; }

}
