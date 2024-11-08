namespace CanonEos.EdSdk.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsFocusPoint
{
    public uint Valid;
    public uint Selected;
    public uint JustFocus;
    public EdsRect Rect;
    public uint Reserved;
}