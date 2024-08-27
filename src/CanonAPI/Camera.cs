namespace CanonAPI;

public sealed class Camera
{
    private IntPtr cptr;

    internal Camera(IntPtr cptr)
    {
        this.cptr = cptr;
        CanonSDK.EdsGetDeviceInfo(cptr, out EdsDeviceInfo info);

        this.Name = info.szDeviceDescription;

        CanonSDK.EdsOpenSession(cptr);

        CanonSDK.DebugProperties(cptr);

        ProductName = CanonSDK.GetStringProperty(cptr, PropertyID.ProductName);
        OwnerName = CanonSDK.GetStringProperty(cptr, PropertyID.OwnerName);
        FirmwareVersion = CanonSDK.GetStringProperty(cptr, PropertyID.FirmwareVersion);
        CurrentStorage = CanonSDK.GetStringProperty(cptr, PropertyID.CurrentStorage);
        CurrentFolder = CanonSDK.GetStringProperty(cptr, PropertyID.CurrentFolder);
        BodyIDEx = CanonSDK.GetStringProperty(cptr, PropertyID.BodyIDEx);
        LensName = CanonSDK.GetStringProperty(cptr, PropertyID.LensName);
        Artist = CanonSDK.GetStringProperty(cptr, PropertyID.Artist);
        Copyright = CanonSDK.GetStringProperty(cptr, PropertyID.Copyright);
        //Artist = CanonSDK.GetStringProperty(cptr, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(cptr, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(cptr, PropertyID.Artist);


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




}
