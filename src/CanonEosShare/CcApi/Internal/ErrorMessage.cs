namespace CanonEos.CcApi.Internal;

internal class ErrorMessage
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
