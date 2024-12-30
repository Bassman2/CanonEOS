namespace CanonEos.CcApi.Internal;

internal class CcDisplayOff
{
    [JsonPropertyName("value")]
    public DisplayOff? Value { get; set; }

    [JsonPropertyName("ability")]
    public List<DisplayOff>? Ability { get; set; }
}

