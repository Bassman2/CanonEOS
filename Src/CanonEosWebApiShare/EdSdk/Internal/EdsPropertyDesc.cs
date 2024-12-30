namespace CanonEos.EdSdk.Internal;

[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(EdsPropertyDescMarshaller))]
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

[CustomMarshaller(typeof(EdsPropertyDesc), MarshalMode.Default, typeof(EdsPropertyDescMarshaller))]
internal static unsafe class EdsPropertyDescMarshaller
{
    public struct EdsPropertyDescUnmanaged
    {
        public int Form;
        public int Access;
        public int NumElements;
        public int* PropDesc;
    }

    public static EdsPropertyDesc ConvertToManaged(EdsPropertyDescUnmanaged unmanaged)
    {
        return new EdsPropertyDesc
        {
            Form = unmanaged.Form,
            Access = unmanaged.Access,
            NumElements = unmanaged.NumElements,
            PropDesc = ArrayMarshaller<int, int>.AllocateContainerForManagedElements(unmanaged.PropDesc, 128)!
        };
    }

    public static EdsPropertyDescUnmanaged ConvertToUnmanaged(EdsPropertyDesc managed)
    {
        return new EdsPropertyDescUnmanaged
        {
            Form = managed.Form,
            Access = managed.Access,
            NumElements = managed.NumElements,
            PropDesc = ArrayMarshaller<int, int>.AllocateContainerForUnmanagedElements(managed.PropDesc, out int _)
        };
    }

    public static void Free(EdsPropertyDescUnmanaged unmanaged)
    {
        ArrayMarshaller<int, int>.Free(unmanaged.PropDesc);
    }
}
