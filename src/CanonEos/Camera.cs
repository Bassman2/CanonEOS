namespace CanonEos;

public interface Camera : IDisposable
{

    bool IsOpen { get; }

    // public bool Open(string host);
    // public bool Open(nint camera);
    void Close();



    #region information

    ConnectionType ConnectionType { get; }
    string? Name { get; }
    string? ProductName { get; }
    string? FirmwareVersion { get; }
    string? BodyIDEx { get; }
    string? LensName { get; }
    string? CurrentStorage { get; }
    string? CurrentFolder { get; }
    TemperatureStatus? TemperatureStatus { get; }

    IEnumerable<BatteryInfo>? Batteries { get; }
    IEnumerable<Volume>? Volumes { get; }
    IEnumerable<Property> Properties { get; }
    
    #endregion

    #region settings

    string? Copyright { get; set; }
    string? Author { get; set; }
    string? OwnerName { get; set; }
    string? Nickname { get; set; }
    DateTime? DateTime { get; set; }
    Beep? Beep { get; set; }
    DisplayOff? DisplayOff { get; set; }
    AutoPowerOff? AutoPowerOff { get; set; }

    #endregion
}
