namespace CanonEos.CcApi.Internal;

internal class CameraDateTime
{
    [JsonPropertyName("datetime")]
    public string? DateTime { get; set; }

    [JsonPropertyName("dst")]
    public bool Dst { get; set; }
}
