using CanonEos.CcApi.Internal;

namespace CanonEos.EdSdk;

internal class EdCamera : Camera
{
    private readonly nint camera;

    private static EdsObjectEventHandler? EdsObjectEvent;

    internal EdCamera(nint camera)
    {
        this.camera = camera;

        Eds.CheckError(Eds.EdsGetDeviceInfo(this.camera, out EdsDeviceInfo info));
        this.Name = info.DeviceDescription;

        Debug.WriteLine($"EdsOpenSession {Name}");
        Eds.CheckError(Eds.EdsOpenSession(this.camera));

        EdsObjectEvent = new EdsObjectEventHandler(OnObjectEvent);
        Eds.CheckError(Eds.EdsSetObjectEventHandler(this.camera, EdsObjectEventID.All, EdsObjectEvent, nint.Zero));


        //Eds.DebugProperties(this.camera);

        ProductName = Eds.GetPropertyString(this.camera, EdsPropertyID.ProductName);
        OwnerName = Eds.GetPropertyString(this.camera, EdsPropertyID.OwnerName);
        FirmwareVersion = Eds.GetPropertyString(this.camera, EdsPropertyID.FirmwareVersion);
        CurrentStorage = Eds.GetPropertyString(this.camera, EdsPropertyID.CurrentStorage);
        CurrentFolder = Eds.GetPropertyString(this.camera, EdsPropertyID.CurrentFolder);
        BodyIDEx = Eds.GetPropertyString(this.camera, EdsPropertyID.BodyIDEx);
        LensName = Eds.GetPropertyString(this.camera, EdsPropertyID.LensName);
        Artist = Eds.GetPropertyString(this.camera, EdsPropertyID.Artist);
        Copyright = Eds.GetPropertyString(this.camera, EdsPropertyID.Copyright);
        //Artist = CanonSDK.GetPropertyString(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetPropertyString(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetPropertyString(camera, PropertyID.Artist);
    }

    public override void Dispose()
    {
        Debug.WriteLine($"EdsCloseSession {Name}");
        Eds.CheckError(Eds.EdsCloseSession(this.camera));
    }

    private EdsError OnObjectEvent(EdsObjectEventID inEvent, IntPtr inRef, IntPtr inContext)
    {
        return 0;
    }

    public override string Name { get; }
    public override string? ProductName { get; }
    public override string? OwnerName { get; set; }
    public override string? FirmwareVersion { get; }
    public override string? CurrentStorage { get; }
    public override string? CurrentFolder { get; }
    public override string? BodyIDEx { get; }
    public override string? LensName { get; }
    public override string? Artist { get; set; }
    public override string? Copyright { get; set; }

    public override DateTime? DateTime { get; set; }
    
    public override IEnumerable<BatteryInfo>? Batteries
    {
        get
        {
            uint level0 = Eds.GetPropertyUInt(camera, EdsPropertyID.BatteryLevel);
            uint quali0 = Eds.GetPropertyUInt(camera, EdsPropertyID.BatteryQuality);
            uint level1 = Eds.GetPropertyUInt(camera, EdsPropertyID.BatteryLevel, 1);
            uint quali1 = Eds.GetPropertyUInt(camera, EdsPropertyID.BatteryQuality, 1);
            return new BatteryInfo[] { new BatteryInfo(level0, quali0), new BatteryInfo(level0, quali0) };
            }
    }
        

    public override IEnumerable<Volume> Volumes { get => Eds.GetChildren(this.camera).Select(i => new EdVolume(i)); }
    

    public override IEnumerable<Property> Properties { get => Eds.GetCameraProperties(this.camera); }
}
