namespace CanonEos;

public class BatteryInfo
{
    internal BatteryInfo(uint level, uint quality)
    {
        Level = level == 0xffffffff ? "AC power" : level.ToString();
        Quality = ((EdsBatteryQuality)quality).ToString();
    }

    internal BatteryInfo(DeviceStatusBattery battery)
    {
        Position = battery.Position;
        Name = battery.Name;
        Kind = battery.Kind.ToString();
        Level = battery.Level;
        Quality = battery.Quality.ToString();
    }

    public DeviceStatusBatteryPosition? Position { get; }

    public string? Name { get; }

    public string? Kind { get; }

    public string? Level { get; }

    public string? Quality { get; }
}
