namespace CanonEos.CcApi.Internal;

internal class PathList
{
    [JsonPropertyName("path")]
    public List<string>? Paths { get; set; }
}
