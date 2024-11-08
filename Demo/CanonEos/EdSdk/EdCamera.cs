namespace CanonEos.EdSdk;

public class EdCamera : Camera
{
    private nint camera = nint.Zero;
    private static EdsObjectEventHandler? EdsObjectEvent;

    public EdCamera()
    { }

    internal EdCamera(nint camera)
    {
        _ = Open(camera) ? 0 : throw new CanonException($"Failed to open {camera}");
    }

    public void Dispose() => Close();

    public bool IsOpen => camera != nint.Zero;

    public bool Open(nint camera)
    {
        this.camera = camera;

        if (Eds.EdsGetDeviceInfo(this.camera, out EdsDeviceInfo info) != EdsError.OK)
        { 
            return false; 
        }
        
        this.Name = info.DeviceDescription;
        Debug.WriteLine($"EdsOpenSession {Name}");

        return Eds.EdsOpenSession(this.camera) == EdsError.OK;
    }

    public void Close()
    {
        if (this.camera != nint.Zero)
        {
            Eds.EdsCloseSession(this.camera);
            this.camera = nint.Zero;
        }
    }


    private EdsError OnObjectEvent(EdsObjectEventID inEvent, IntPtr inRef, IntPtr inContext)
    {
        return 0;
    }

    #region information

    public ConnectionType ConnectionType => ConnectionType.USB;
    public string? Name { get; private set; }
    public string? ProductName => Eds.GetPropertyString(this.camera, EdsPropertyID.ProductName);
    public string? FirmwareVersion => Eds.GetPropertyString(this.camera, EdsPropertyID.FirmwareVersion);
    public string? BodyIDEx => Eds.GetPropertyString(this.camera, EdsPropertyID.BodyIDEx);
    public string? LensName => Eds.GetPropertyString(this.camera, EdsPropertyID.LensName);
    public string? CurrentStorage => Eds.GetPropertyString(this.camera, EdsPropertyID.CurrentStorage);
    public string? CurrentFolder => Eds.GetPropertyString(this.camera, EdsPropertyID.CurrentFolder);

    public TemperatureStatus? TemperatureStatus => (TemperatureStatus)Eds.GetPropertyInt(this.camera, EdsPropertyID.TempStatus);

    public IEnumerable<BatteryInfo>? Batteries
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

    public IEnumerable<EosVolume> Volumes { get => Eds.GetChildren(this.camera).Select(i => new EdVolume(i)); }

    public IEnumerable<Property> Properties { get => Eds.GetCameraProperties(this.camera); }

    #endregion

    #region settings

    public string? Copyright
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.Copyright);
        set => Eds.SetProperty(this.camera, EdsPropertyID.Copyright, value);
    }
    public string? Author
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.Artist);
        set => Eds.SetProperty(this.camera, EdsPropertyID.Artist, value);
    }

    public string? Owner
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.OwnerName);
        set => Eds.SetProperty(this.camera, EdsPropertyID.OwnerName, value);
    }

    public string? Nickname 
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.Unknown);
        set => Eds.SetProperty(this.camera, EdsPropertyID.Unknown, value);
    }

    public DateTime? DateTime
    {
        get => (DateTime)Eds.GetPropertyStruct<EdsTime>(this.camera, EdsPropertyID.DateTime);
        set => Eds.SetProperty(this.camera, EdsPropertyID.DateTime, value);
    }

    public string? Beep
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.Unknown);
        set => Eds.SetProperty(this.camera, EdsPropertyID.Unknown, value);
    }

    public string[]? BeepValues => [];

    public string? DisplayOff
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.Unknown);
        set => Eds.SetProperty(this.camera, EdsPropertyID.Unknown, value);
    }

    public string[]? DisplayOffValues => [];

    public string? AutoPowerOff
    {
        get => Eds.GetPropertyString(this.camera, EdsPropertyID.AutoPowerOffSetting);
        set => Eds.SetProperty(this.camera, EdsPropertyID.AutoPowerOffSetting, value);
    }

    public string[]? AutoPowerOffValues => [];

    #endregion

}
