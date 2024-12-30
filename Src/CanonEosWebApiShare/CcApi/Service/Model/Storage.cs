namespace CanonEos.CcApi.Internal;

internal class Storage
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // ver 1.0.0
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    // >= ver 1.1.0
    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("accesscapability")]
    public string? AccessCapability { get; set; }

    [JsonPropertyName("maxize")]
    public ulong Maxize { get; set; }

    [JsonPropertyName("spacesize")]
    public ulong SpaceSize { get; set; }

    [JsonPropertyName("contentsnumber")]
    public ulong ContentsNumber { get; set; }
}
