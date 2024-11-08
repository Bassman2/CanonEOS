namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential, Pack = 2)]
[NativeMarshalling(typeof(EdsCapacityMarshaller))]
internal struct EdsCapacity
{
    /// <summary>
    /// Number of free clusters on the HD
    /// </summary>
    public int NumberOfFreeClusters;
    /// <summary>
    /// Bytes per HD sector
    /// </summary>
    public int BytesPerSector;
    /// <summary>
    /// Reset flag
    /// </summary>
    public bool Reset;
}

[CustomMarshaller(typeof(EdsCapacity), MarshalMode.Default, typeof(EdsCapacityMarshaller))]
internal static unsafe class EdsCapacityMarshaller
{
    public struct EdsCapacityUnmanaged
    {
        public int NumberOfFreeClusters;
        public int BytesPerSector;
        public int Reset;
    }

    public static EdsCapacity ConvertToManaged(EdsCapacityUnmanaged unmanaged)
    {
        return new EdsCapacity
        {
            NumberOfFreeClusters = unmanaged.NumberOfFreeClusters,
            BytesPerSector = unmanaged.BytesPerSector,
            Reset = unmanaged.Reset != 0
        };
    }

    public static EdsCapacityUnmanaged ConvertToUnmanaged(EdsCapacity managed)
    {
        return new EdsCapacityUnmanaged
        {
            NumberOfFreeClusters = managed.NumberOfFreeClusters,
            BytesPerSector = managed.BytesPerSector,
            Reset = managed.Reset ? 1 : 0
        };
    }

    public static void Free(EdsCapacityUnmanaged unmanaged)
    { }
}
