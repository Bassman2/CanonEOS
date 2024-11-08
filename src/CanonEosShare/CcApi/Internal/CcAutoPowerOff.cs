namespace CanonEos.CcApi.Internal;

internal class CcAutoPowerOff
{
    [JsonPropertyName("value")]
    public AutoPowerOff? Value { get; set; }

    [JsonPropertyName("ability")]
    public List<AutoPowerOff>? Ability { get; set; }
}


