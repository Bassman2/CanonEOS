namespace CanonEos.CcApi.Internal;

internal class Batteries
{
    [JsonPropertyName("batterylist")]
    public List<Battery110>? BatteryList { get; set; }
}

internal class Battery110
{
    [JsonPropertyName("position")]
    public string? Position { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }
}

public enum BatteryPosition
{
    camera,
    grip01,
    grip02
}
