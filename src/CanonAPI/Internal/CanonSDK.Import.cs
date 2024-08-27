namespace CanonAPI.Internal;

internal partial class CanonSDK
{
    private const string DllPath = "EDSDK.dll";

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsInitializeSDK();

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsTerminateSDK();

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsRetain(IntPtr inRef);

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsRelease(IntPtr inRef);

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsGetCameraList(out IntPtr outCameraListRef);

    //[LibraryImport(DllPath)]
    //internal static partial EdsError EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DllPath)]
    public extern static EdsError EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DllPath)]
    public extern static EdsError EdsGetChildCount(IntPtr inRef, out int outCount);

    [DllImport(DllPath)]
    public extern static EdsError EdsGetChildAtIndex(IntPtr inRef, int inIndex, out IntPtr outRef);

    [DllImport(DllPath)]
    public extern static EdsError EdsGetPropertySize(IntPtr inRef, PropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [DllImport(DllPath)]
    public extern static EdsError EdsGetPropertyData(IntPtr inRef, PropertyID inPropertyID, int inParam, int inPropertySize, IntPtr outPropertyData);

    [DllImport(DllPath)]
    public extern static EdsError EdsOpenSession(IntPtr inCameraRef);

    /// <summary>
    /// Closes a logical connection with a remote camera.
    /// </summary>
    /// <param name="inCameraRef">The reference of the camera.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(DllPath)]
    public extern static EdsError EdsCloseSession(IntPtr inCameraRef);

    [DllImport(DllPath)]
    public extern static EdsError EdsSetCameraAddedHandler(SDKCameraAddedHandler inCameraAddedHandler, IntPtr inContext);

    [LibraryImport(DllPath)]
    internal static partial EdsError EdsSendCommand(IntPtr inCameraRef, EdsCameraCommand inCommand, int inParam);

    /// <summary>
    /// Sets the remote camera state or mode.
    /// </summary>
    /// <param name="inCameraRef">The reference of the camera which will receive the command.</param>
    /// <param name="inCameraState">Specifies the command to be sent.</param>
    /// <param name="inParam">Specifies additional command-specific information.</param>
    /// <returns>Any of the SDK errors</returns>
    [LibraryImport(DllPath)]
    internal static partial EdsError EdsSendStatusCommand(IntPtr inCameraRef, EdsCameraStatusCommand inCameraState, int inParam);


    [DllImport(DllPath)]
    public extern static EdsError EdsGetVolumeInfo(IntPtr inCameraRef, out EdsVolumeInfo outVolumeInfo);

    /// <summary>
    /// Formats a volume.
    /// </summary>
    /// <param name="inVolumeRef">The reference of the volume.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(DllPath)]
    public extern static EdsError EdsFormatVolume(IntPtr inVolumeRef);

    /// <summary>
    /// Gets information about the directory or file object on the memory card (volume) in a remote camera.
    /// </summary>
    /// <param name="inDirItemRef">The reference of the directory item.</param>
    /// <param name="outDirItemInfo">Information of the directory item.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(DllPath)]
    public extern static EdsError EdsGetDirectoryItemInfo(IntPtr inDirItemRef, out EdsDirectoryItemInfo outDirItemInfo);

    /// <summary>
    /// Deletes a camera folder or file.
    /// If folders with subdirectories are designated, all files are deleted except protected files.
    /// DirectoryItem objects deleted by means of this method are implicitly released.
    /// Thus, there is no need to release them by means of Release.
    /// </summary>
    /// <param name="inDirItemRef"></param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(DllPath)]
    public extern static EdsError EdsDeleteDirectoryItem(IntPtr inDirItemRef);

}
