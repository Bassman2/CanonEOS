namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential, Pack = 2)]
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

