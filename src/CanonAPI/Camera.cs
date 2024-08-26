using CanonAPI.Internal;

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
        ProductName = CanonSDK.GetStringProperty(cptr, PropertyID.ProductName);
        FirmwareVersion = CanonSDK.GetStringProperty(cptr, PropertyID.FirmwareVersion);

    }

    public string Name { get; }
    public string? ProductName { get; }

    public string? FirmwareVersion { get; }




}
