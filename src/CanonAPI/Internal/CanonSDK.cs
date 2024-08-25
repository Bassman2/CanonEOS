using System.Runtime.InteropServices;

namespace CanonAPI.Internal;

internal partial class CanonSDK
{
    private const string DLLPath = "EDSDK.dll";

    [LibraryImport(DLLPath)]
    internal static partial uint EdsInitializeSDK();

    [LibraryImport(DLLPath)]
    internal static partial uint EdsTerminateSDK();

    [LibraryImport(DLLPath)]
    internal static partial uint EdsRetain(IntPtr inRef);

    [LibraryImport(DLLPath)]
    internal static partial uint EdsRelease(IntPtr inRef);

    [LibraryImport(DLLPath)]
    internal static partial uint EdsGetCameraList(out IntPtr outCameraListRef);

    //[LibraryImport(DLLPath)]
    //internal static partial uint EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DLLPath)]
    public extern static uint EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(DLLPath)]
    public extern static uint EdsGetChildCount(IntPtr inRef, out int outCount);

    [DllImport(DLLPath)]
    public extern static uint EdsGetChildAtIndex(IntPtr inRef, int inIndex, out IntPtr outRef);
}
