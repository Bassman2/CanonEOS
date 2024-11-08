namespace CanonEos.CcApi.Internal;

internal class DeviceStatusStorage
{
    [JsonPropertyName("storagelist")]
    public List<Storage>? Storages { get; set; }
}