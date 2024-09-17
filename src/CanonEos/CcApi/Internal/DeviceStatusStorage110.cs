namespace CanonEos.CcApi.Internal;

internal class DeviceStatusStorage110
{
    [JsonPropertyName("storagelist")]
    public List<Storage110>? Storages { get; set; }
}