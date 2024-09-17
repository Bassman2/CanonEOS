namespace CanonEos.CcApi.Internal;

internal class DeviceInformation
{
    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("productname")]
    public string? ProductName { get; set; }

    [JsonPropertyName("guid")]
    public string? Guid { get; set; }

    [JsonPropertyName("serialnumber")]
    public string? SerialNumber { get; set; }

    [JsonPropertyName("macaddress")]
    public string? MacAddress { get; set; }
    
    [JsonPropertyName("firmwareversion")]
    public string? FirmwareVersion { get; set; }
}
