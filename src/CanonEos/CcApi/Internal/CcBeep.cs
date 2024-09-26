namespace CanonEos.CcApi.Internal;

internal class CcBeep
{
    [JsonPropertyName("value")]
    public Beep? Value { get; set; }

    [JsonPropertyName("ability")]
    public List<Beep>? Ability { get; set; }
}


