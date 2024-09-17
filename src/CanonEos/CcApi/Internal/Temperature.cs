namespace CanonEos.CcApi.Internal;

internal class Temperature
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public enum TemperatureStatus
{
    Normal,
    Warning,
    Frameratedown,
    Disableliveview,
    Disablerelease,
    Stillqualitywarning,
    Restrictionmovierecording,
    WarningAndRestrictionmovierecording,
    FrameratedownAndRestrictionmovierecording,
    DisableliveviewAndRestrictionmovierecording,
    DisablereleaseAndRestrictionmovierecording,
    StillqualitywarningAndRestrictionmovierecording

}