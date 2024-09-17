namespace CanonEos.CcApi.Internal;

internal class Lens
{
    [JsonPropertyName("mount")]
    public bool Mount { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

}
