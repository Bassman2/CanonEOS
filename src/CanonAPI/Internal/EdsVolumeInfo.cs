namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsVolumeInfo
{
    public EdsStorageType StorageType;
    public uint Access;
    public ulong MaxCapacity;
    public ulong FreeSpaceInBytes;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string szVolumeLabel;
}
