namespace CanonEos.EdSdk;

public enum EdsStorageType
{
    [Description("No memory card inserted")]
    No = 0,
    [Description("Compact flash")]
    CompactFlash = 1,
    [Description("SD card")]
    SDCard = 2
}
