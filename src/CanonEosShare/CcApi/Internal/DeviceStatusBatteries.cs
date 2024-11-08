namespace CanonEos.CcApi.Internal;

internal class DeviceStatusBatteries
{
    [JsonPropertyName("batterylist")]
    public List<DeviceStatusBattery>? Batteries { get; set; }
}


