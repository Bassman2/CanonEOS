namespace CanonAPI;

public sealed class Camera
{
    private IntPtr camera;

    internal Camera(IntPtr camera)
    {
        this.camera = camera;
        CanonSDK.EdsGetDeviceInfo(camera, out EdsDeviceInfo info);

        this.Name = info.szDeviceDescription;

        CanonSDK.EdsOpenSession(camera);

        CanonSDK.DebugProperties(camera);

        ProductName = CanonSDK.GetStringProperty(camera, PropertyID.ProductName);
        OwnerName = CanonSDK.GetStringProperty(camera, PropertyID.OwnerName);
        FirmwareVersion = CanonSDK.GetStringProperty(camera, PropertyID.FirmwareVersion);
        CurrentStorage = CanonSDK.GetStringProperty(camera, PropertyID.CurrentStorage);
        CurrentFolder = CanonSDK.GetStringProperty(camera, PropertyID.CurrentFolder);
        BodyIDEx = CanonSDK.GetStringProperty(camera, PropertyID.BodyIDEx);
        LensName = CanonSDK.GetStringProperty(camera, PropertyID.LensName);
        Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        Copyright = CanonSDK.GetStringProperty(camera, PropertyID.Copyright);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);


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
        get => CanonSDK.GetChildren(camera).Select(i => new Volume(i));
    }
}
