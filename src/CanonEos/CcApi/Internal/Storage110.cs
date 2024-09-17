namespace CanonEos.CcApi.Internal;

internal class Storage110
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

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
