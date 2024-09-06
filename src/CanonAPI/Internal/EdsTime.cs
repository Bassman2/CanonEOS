namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsTime
{
    public uint Year;
    public uint Month;
    public uint Day;
    public uint Hour;
    public uint Minute;
    public uint Second;
    public uint Milliseconds;

    public static explicit operator DateTime(EdsTime time) => 
        new DateTime((int) time.Year, (int) time.Month, (int) time.Day, (int) time.Hour, (int) time.Minute, (int) time.Second, (int) time.Milliseconds);
}
