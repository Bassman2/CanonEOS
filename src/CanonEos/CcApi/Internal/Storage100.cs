namespace CanonEos.CcApi.Internal;

internal class Storage100
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("accesscapability")]
    public string? AccessCapability { get; set; }

    [JsonPropertyName("maxize")]
    public ulong Maxize { get; set; }

    [JsonPropertyName("spacesize")]
    public ulong SpaceSize { get; set; }

    [JsonPropertyName("contentsnumber")]
    public ulong ContentsNumber { get; set; }
}
