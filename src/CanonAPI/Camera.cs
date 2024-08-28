namespace CanonAPI;

public sealed class Camera : IDisposable
{
    private readonly nint camera;
    
    internal Camera(nint camera) 
    {
        this.camera = camera;

        EdsNativeLib.EdsGetDeviceInfo(this.camera, out EdsDeviceInfo info);
        this.Name = info.szDeviceDescription;

        Debug.WriteLine($"EdsOpenSession {Name}");
        EdsNativeLib.EdsOpenSession(this.camera);

        EdsNativeLib.DebugProperties(this.camera);

        ProductName = EdsNativeLib.GetStringProperty(this.camera, PropertyID.ProductName);
        OwnerName = EdsNativeLib.GetStringProperty(this.camera, PropertyID.OwnerName);
        FirmwareVersion = EdsNativeLib.GetStringProperty(this.camera, PropertyID.FirmwareVersion);
        CurrentStorage = EdsNativeLib.GetStringProperty(this.camera, PropertyID.CurrentStorage);
        CurrentFolder = EdsNativeLib.GetStringProperty(this.camera, PropertyID.CurrentFolder);
        BodyIDEx = EdsNativeLib.GetStringProperty(this.camera, PropertyID.BodyIDEx);
        LensName = EdsNativeLib.GetStringProperty(this.camera, PropertyID.LensName);
        Artist = EdsNativeLib.GetStringProperty(this.camera, PropertyID.Artist);
        Copyright = EdsNativeLib.GetStringProperty(this.camera, PropertyID.Copyright);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
    }

    public void Dispose()
    {
        Debug.WriteLine($"EdsCloseSession {Name}");
        EdsNativeLib.EdsCloseSession(this.camera); 
    }

    public string Name { get; }
    public string? ProductName { get; }
    public string? OwnerName { get; }
    public string? FirmwareVersion { get; }
    public string? CurrentStorage { get; }
    public string? CurrentFolder { get; }
    public string? BodyIDEx { get; }
    public string? LensName { get; }
    public string? Artist { get; }
    public string? Copyright { get; }


    public IEnumerable<Volume> Volumes
    {
        get => EdsNativeLib.GetChildren(this.camera).Select(i => new Volume(i));
    }    
}
