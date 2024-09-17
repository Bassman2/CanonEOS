using System.Runtime.Serialization;

namespace CanonEos.CcApi.Internal;

internal class Battery
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }
}

public enum BatteryKind
{
    Battery,
    NotInserted,
    AcAdapter,
    DcCoupler,
    Unknown,
    // only for devicestatus/battery
    Batterygrip
}

public enum BatteryLevel
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

public enum BatteryQuality
{
    Bad,
    Normal,
    Good,
    Unknown
}