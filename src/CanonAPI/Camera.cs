using System;

namespace CanonAPI;

public sealed class Camera : IDisposable
{
    private readonly nint camera;

    private static EdsObjectEventHandler? EdsObjectEvent;

    private EdsError OnObjectEvent(EdsObjectEventID inEvent, IntPtr inRef, IntPtr inContext)
    {
        return 0;
    }

    internal Camera(nint camera) 
    {
        this.camera = camera;

        Eds.CheckError(Eds.EdsGetDeviceInfo(this.camera, out EdsDeviceInfo info));
        this.Name = info.DeviceDescription;

        Debug.WriteLine($"EdsOpenSession {Name}");
        Eds.CheckError(Eds.EdsOpenSession(this.camera));

        EdsObjectEvent = new EdsObjectEventHandler(OnObjectEvent);
        Eds.CheckError(Eds.EdsSetObjectEventHandler(this.camera, EdsObjectEventID.All, EdsObjectEvent, nint.Zero));


        Eds.DebugProperties(this.camera);

        ProductName = Eds.GetStringProperty(this.camera, EdsPropertyID.ProductName);
        OwnerName = Eds.GetStringProperty(this.camera, EdsPropertyID.OwnerName);
        FirmwareVersion = Eds.GetStringProperty(this.camera, EdsPropertyID.FirmwareVersion);
        CurrentStorage = Eds.GetStringProperty(this.camera, EdsPropertyID.CurrentStorage);
        CurrentFolder = Eds.GetStringProperty(this.camera, EdsPropertyID.CurrentFolder);
        BodyIDEx = Eds.GetStringProperty(this.camera, EdsPropertyID.BodyIDEx);
        LensName = Eds.GetStringProperty(this.camera, EdsPropertyID.LensName);
        Artist = Eds.GetStringProperty(this.camera, EdsPropertyID.Artist);
        Copyright = Eds.GetStringProperty(this.camera, EdsPropertyID.Copyright);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
        //Artist = CanonSDK.GetStringProperty(camera, PropertyID.Artist);
    }

    public void Dispose()
    {
        Debug.WriteLine($"EdsCloseSession {Name}");
        Eds.CheckError(Eds.EdsCloseSession(this.camera)); 
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
        get => Eds.GetChildren(this.camera).Select(i => new Volume(i));
    }   
    
    public IEnumerable<Property> Properties
    { 
        get => Eds.GetProperties(this.camera); 
    }
}

public class Property(EdsPropertyID id, EdsDataType dataType, object value)
{
    public EdsPropertyID Id { get; } = id;
    public EdsDataType DataType { get; } = dataType;
    public object Value { get; } = value;

    public string ValueString { get => value.ToString()!; }
}
