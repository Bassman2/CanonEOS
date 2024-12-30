namespace CanonEos.CcApi.Internal;

internal class ImageInfo
{
    [JsonPropertyName("filesize")]
    public ulong filesize { get; set; }

    [JsonPropertyName("protect")]
    public string? Protect { get; set; }

    [JsonPropertyName("archive")]
    public string? Archive { get; set; }

    [JsonPropertyName("rotate")]
    public string? Rotate { get; set; }

    [JsonPropertyName("rating")]
    public string? Rating { get; set; }

    [JsonPropertyName("lastmodifieddate")]
    public string? LastModifiedDate { get; set; }

    /// <summary>
    /// Play time (seconds) * null for other than movies.
    /// </summary>
    [JsonPropertyName("playtime")]
    public ulong? Playtime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Ver 1.1.0</remarks>
    [JsonPropertyName("hdr")]
    public string? Hdr { get; set; }

}
