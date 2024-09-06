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
}
