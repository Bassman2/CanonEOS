namespace CanonEos.CcApi.Internal;

internal class DeviceStatusStorage100
{
    [JsonPropertyName("storagelist")]
    public List<Storage100>? Storages { get; set; }
}

