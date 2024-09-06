namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct EdsFocusInfo
{
    public EdsRect ImageRect;
    public uint PointNumber;
    //public fixed EdsFocusPoint FocusPoint[1053];
    //public uint ExecuteMode;
}