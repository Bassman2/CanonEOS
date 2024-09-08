using System;

namespace CanonAPI;

public class Camera : IDisposable
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

public class Property(EdsPropertyID id, int param, EdsDataType dataType, object? value)
{
    public EdsPropertyID Id { get; } = id;
    public int Param { get; } = param;

    public EdsDataType DataType { get; } = dataType;
    public object? Value { get; } = value ?? null;

    public string ValueString
    {
        get
        {
            if (value == null)
            {
                return "";
            }
            if (value.GetType().IsArray)
            {
                string res = "";
                Array arr = (Array)value;
                foreach (object item in arr)
                {
                    res += item.ToString() + ",";
                }
                return res.Trim(',');
            }
            //if (value.GetType() == typeof(EdsFocusInfo))
            //{ }
            //if (value.GetType() == typeof(EdsPictureStyleDesc))
            //{ }

            return value?.ToString() ?? "";
        }
    }
}
