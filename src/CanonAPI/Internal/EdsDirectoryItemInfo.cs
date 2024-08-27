namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
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
