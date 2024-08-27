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

}
