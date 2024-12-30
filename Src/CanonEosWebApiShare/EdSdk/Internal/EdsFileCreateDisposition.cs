namespace CanonEos.EdSdk.Internal;


public enum EdsFileCreateDisposition : int
{
    /// <summary>
    /// Creates a new file. An error occurs if the designated file already exists
    /// </summary>
    CreateNew = 0,
    /// <summary>
    /// Creates a new file. If the designated file already
    /// exists, that file is overwritten and existing attributes
    /// </summary>
    CreateAlways = 1,
    /// <summary>
    /// Opens a file. An error occurs if the designated file does not exist.
    /// </summary>
    OpenExisting = 2,
    /// <summary>
    /// If the file exists, it is opened. If the designated file
    /// does not exist, a new file is created.
    /// </summary>
    OpenAlways = 3,
    /// <summary>
    /// Opens a file and sets the file size to 0 bytes.
    /// </summary>
    TruncateExisting = 4,
}