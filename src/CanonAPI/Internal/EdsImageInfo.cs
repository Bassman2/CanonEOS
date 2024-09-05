namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential)]
internal struct EdsImageInfo
{
    /// <summary>
    /// Width of image
    /// </summary>
    public int Width;
    /// <summary>
    /// Height of image
    /// </summary>
    public int Height;
    /// <summary>
    /// Number of channels
    /// </summary>
    public int NumOfComponents;
    /// <summary>
    /// Bitdepth of channels
    /// </summary>
    public int ComponentDepth;
    /// <summary>
    /// Effective size of image
    /// </summary>
    public EdsRectangle EffectiveRect;
    public uint Reserved1;
    public uint Reserved2;
}

