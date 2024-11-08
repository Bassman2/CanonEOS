namespace CanonEos.EdSdk.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsRect
{
    public EdsPoint Point;
    public EdsSize Size;
    public int Width;
}