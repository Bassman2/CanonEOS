namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(EdsDirectoryItemInfoMarshaller))]
internal struct EdsDirectoryItemInfo
{
    /// <summary>
    /// Size of directory item (as long)
    /// </summary>
    public long Size64;
    /// <summary>
    /// Marker if it's a folder or a file
    /// </summary>
    public bool IsFolder;
    /// <summary>
    /// Group ID
    /// </summary>
    public int GroupID;
    /// <summary>
    /// Option
    /// </summary>
    public int Option;
    /// <summary>
    /// File name
    /// </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string FileName;
    /// <summary>
    /// Format
    /// </summary>
    public EdsImageType Format;
    /// <summary>
    /// Date time
    /// </summary>
    public int DateTime;
}

[CustomMarshaller(typeof(EdsDirectoryItemInfo), MarshalMode.Default, typeof(EdsDirectoryItemInfoMarshaller))]
internal static unsafe class EdsDirectoryItemInfoMarshaller
{
    public struct EdsDirectoryItemInfoUnmanaged
    {
        public long Size64;
        public int IsFolder;
        public int GroupID;
        public int Option;
        public byte* FileName;
        public int Format;
        public int DateTime;
    }

    public static EdsDirectoryItemInfo ConvertToManaged(EdsDirectoryItemInfoUnmanaged unmanaged)
    {
        return new EdsDirectoryItemInfo
        {
            Size64 = unmanaged.Size64,
            IsFolder = unmanaged.IsFolder != 0,
            GroupID = unmanaged.GroupID,
            Option = unmanaged.Option,
            FileName = Utf8StringMarshaller.ConvertToManaged(unmanaged.FileName)!,
            Format = (EdsImageType)unmanaged.Format,
            DateTime = unmanaged.DateTime
        };
    }

    public static EdsDirectoryItemInfoUnmanaged ConvertToUnmanaged(EdsDirectoryItemInfo managed)
    {
        return new EdsDirectoryItemInfoUnmanaged
        {
            Size64 = managed.Size64,
            IsFolder = managed.IsFolder ? 1 : 0,
            GroupID = managed.GroupID,
            Option = managed.Option,
            FileName = Utf8StringMarshaller.ConvertToUnmanaged(managed.FileName),
            Format = (int)managed.Format,
            DateTime = managed.DateTime
        };
    }

    public static void Free(EdsDirectoryItemInfoUnmanaged unmanaged)
    {
        Utf8StringMarshaller.Free(unmanaged.FileName);
    }
}