namespace CanonEos.CcApi.Internal;

internal class CameraDateTime
{
    [JsonPropertyName("datetime")]
    public DateTime? DateTime { get; set; }

    [JsonPropertyName("dst")]
    public bool Dst { get; set; }

    public static implicit operator DateTime?(CameraDateTime? d) 
    {
        return d?.DateTime;
    }

    public static explicit operator CameraDateTime?(DateTime? d)
    {
        return d is null ? null : new CameraDateTime() { DateTime = d };
    }
}
