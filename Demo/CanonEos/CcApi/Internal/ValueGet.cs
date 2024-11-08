namespace CanonEos.CcApi.Internal;

internal class ValueGet
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("ability")]
    public List<string>? Ability { get; set; }
}
