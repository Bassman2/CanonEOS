namespace CanonEos.EdSdk.Internal;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct EdsFocusInfo
{
    public EdsRect ImageRect;
    public uint PointNumber;
    //public fixed EdsFocusPoint FocusPoint[1053];
    //public uint ExecuteMode;

    public override string ToString() => $"Rect ({ImageRect.Point.X},{ImageRect.Point.X},{ImageRect.Size.Width},{ImageRect.Size.Height}), #Point {PointNumber}";

}