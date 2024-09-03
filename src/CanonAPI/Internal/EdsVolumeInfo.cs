namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(EdsVolumeInfoMarshaller))]
internal struct EdsVolumeInfo
{
    public EdsStorageType StorageType;
    public uint Access;
    public ulong MaxCapacity;
    public ulong FreeSpaceInBytes;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string VolumeLabel;
}

[CustomMarshaller(typeof(EdsVolumeInfo), MarshalMode.Default, typeof(EdsVolumeInfoMarshaller))]
internal static unsafe class EdsVolumeInfoMarshaller
{
    public struct EdsVolumeInfoUnmanaged
    {
        public uint StorageType;
        public uint Access;
        public ulong MaxCapacity;
        public ulong FreeSpaceInBytes;
        public fixed byte VolumeLabel[EdsConst.EDS_MAX_NAME];
    }

    public static EdsVolumeInfo ConvertToManaged(EdsVolumeInfoUnmanaged unmanaged)
    {
        return new EdsVolumeInfo
        {
            StorageType = (EdsStorageType)unmanaged.StorageType,
            Access = unmanaged.Access,
            MaxCapacity = unmanaged.MaxCapacity,
            FreeSpaceInBytes = unmanaged.FreeSpaceInBytes,
            VolumeLabel = Utf8StringMarshaller.ConvertToManaged(unmanaged.VolumeLabel)!            
        };
    }

    //public static EdsVolumeInfoUnmanaged ConvertToUnmanaged(EdsVolumeInfo managed)
    //{
    //    return new EdsVolumeInfoUnmanaged
    //    {
    //        StorageType = (uint)managed.StorageType,
    //        Access = managed.Access,
    //        MaxCapacity = managed.MaxCapacity,
    //        FreeSpaceInBytes = managed.FreeSpaceInBytes,
    //        VolumeLabel = Utf8StringMarshaller.ConvertToUnmanaged(managed.VolumeLabel)!
    //    };
    //}

    //public static void Free(EdsVolumeInfoUnmanaged unmanaged)
    //{
    //    Utf8StringMarshaller.Free(unmanaged.VolumeLabel);
    //}
}
