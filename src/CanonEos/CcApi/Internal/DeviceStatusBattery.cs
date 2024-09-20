using System.Runtime.Serialization;

namespace CanonEos.CcApi.Internal;

internal class DeviceStatusBattery
{
    [JsonPropertyName("position")]
    public DeviceStatusBatteryPosition? Position { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("kind")]
    public DeviceStatusBatteryKind? Kind { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("quality")]
    public DeviceStatusBatteryQuality? Quality { get; set; }
}

public enum DeviceStatusBatteryPosition
{
    [EnumMember(Value = "camera")]
    Camera,
    [EnumMember(Value = "grip01")]
    Grip01,
    [EnumMember(Value = "grip02")]
    Grip02
}

public enum DeviceStatusBatteryKind
{
    [EnumMember(Value = "battery")]
    Battery,
    [EnumMember(Value = "not_inserted")]
    NotInserted,
    [EnumMember(Value = "ac_adapter")]
    AcAdapter,
    [EnumMember(Value = "dc_coupler")]
    DcCoupler,
    [EnumMember(Value = "unknown")]
    Unknown,
    // only for devicestatus/battery
    [EnumMember(Value = "batterygrip")]
    Batterygrip
}

public enum DeviceStatusBatteryLevel
{
    Low,
    Quarter,
    Half,
    High,
    Full,
    Unknown,
    Charge, 
    Chargestop,
    Chargecomp
}

public enum DeviceStatusBatteryQuality
{
    [EnumMember(Value = "bad")]
    Bad,
    [EnumMember(Value = "normal")]
    Normal,
    [EnumMember(Value = "good")]
    Good,
    [EnumMember(Value = "unknown")]
    Unknown
}