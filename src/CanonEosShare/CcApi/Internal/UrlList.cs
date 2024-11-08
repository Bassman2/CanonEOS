namespace CanonEos.CcApi.Internal;

internal class UrlList
{
    [JsonPropertyName("url")]
    public List<string>? Urls { get; set; }
}
