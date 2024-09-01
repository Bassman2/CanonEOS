namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct EdsPropertyDesc
{
    /// <summary>
    /// Form
    /// </summary>
    public int Form;
    /// <summary>
    /// Accessibility
    /// </summary>
    public int Access;
    /// <summary>
    /// Number of elements
    /// </summary>
    public int NumElements;
    /// <summary>
    /// Array of all elements
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public int[] PropDesc;
}

