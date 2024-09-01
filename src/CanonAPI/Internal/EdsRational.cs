namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsRational
{
    public int numerator;
    public uint denominator;
}
