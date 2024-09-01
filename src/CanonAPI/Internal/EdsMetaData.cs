namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsMetaData
{
    public byte latitudeRef;
    public byte longitudeRef;
    public byte altitudeRef;
    public byte status;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public EdsRational[] latitude;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public EdsRational[] longitude;
}
