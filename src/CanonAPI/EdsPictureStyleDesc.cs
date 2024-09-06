namespace CanonAPI;

[StructLayout(LayoutKind.Sequential)]
public struct EdsPictureStyleDesc
{
    public int Contrast;
    public uint Sharpness;
    public int Saturation;
    public int ColorTone;
    public uint FilterEffect;
    public uint ToningEffect;
    public uint SharpFineness;
    public uint SharpThreshold;
}
