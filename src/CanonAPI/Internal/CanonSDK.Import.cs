using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CanonAPI.Internal;

internal partial class CanonSDK
{
    private const string DllPath = "EDSDK.dll";

    [LibraryImport(DllPath)]
    internal static partial uint EdsInitializeSDK();

    [LibraryImport(DllPath)]
    internal static partial uint EdsTerminateSDK();

    [LibraryImport(DllPath)]
    internal static partial uint EdsRetain(IntPtr inRef);

    [LibraryImport(DllPath)]
    internal static partial uint EdsRelease(IntPtr inRef);

    [LibraryImport(DllPath)]
    internal static partial uint EdsGetCameraList(out IntPtr outCameraListRef);

    //[LibraryImport(DllPath)]
    //internal static partial uint EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DllPath)]
    public extern static uint EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DllPath)]
    public extern static uint EdsGetChildCount(IntPtr inRef, out int outCount);

    [DllImport(DllPath)]
    public extern static uint EdsGetChildAtIndex(IntPtr inRef, int inIndex, out IntPtr outRef);

    [DllImport(DllPath)]
    public extern static uint EdsGetPropertySize(IntPtr inRef, PropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [DllImport(DllPath)]
    public extern static uint EdsGetPropertyData(IntPtr inRef, PropertyID inPropertyID, int inParam, int inPropertySize, IntPtr outPropertyData);

    [DllImport(DllPath)]
    public extern static uint EdsOpenSession(IntPtr inCameraRef);

    /// <summary>
    /// Closes a logical connection with a remote camera.
    /// </summary>
    /// <param name="inCameraRef">The reference of the camera.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(DllPath)]
    public extern static uint EdsCloseSession(IntPtr inCameraRef);
}
