namespace CanonEos.CcApi.Internal;

internal class TempStatus
{
    [JsonPropertyName("status")]
    public TemperatureStatus? Status { get; set; }
}

