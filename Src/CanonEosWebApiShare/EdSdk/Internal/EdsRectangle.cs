namespace CanonEos.EdSdk.Internal;


[StructLayout(LayoutKind.Sequential)]
public struct EdsRectangle
{
    /// <summary>
    /// X Coordinate
    /// </summary>
    public int X;
    /// <summary>
    /// Y Coordinate
    /// </summary>
    public int Y;
    /// <summary>
    /// Width of the rectangle
    /// </summary>
    public int Width;
    /// <summary>
    /// Height of the rectangle
    /// </summary>
    public int Height;
}
