//#define NATIVE_LIBRARY

using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace CanonAPI.Internal;

internal static unsafe partial class EdsNativeLib
{
    private const string LibName = "EDSDK";

#if NATIVE_LIBRARY

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError InitializeSDK();

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError TerminateSDK();

    private static readonly nint library;

    private static readonly nint edsInitializeSDK;
    private static readonly nint edsTerminateSDK;

    static EdsNativeLib()
    {
        string path = Path.Combine(AppContext.BaseDirectory, LibName, Environment.Is64BitProcess ? "Win64" : "Win32", LibName);
        library = NativeLibrary.Load(path);
        edsInitializeSDK = NativeLibrary.GetExport(library, nameof(EdsInitializeSDK));
        edsTerminateSDK = NativeLibrary.GetExport(library, nameof(EdsTerminateSDK));
    }

    internal static EdsError EdsInitializeSDK()
    {
        return (EdsError)((delegate*<IntPtr>)edsInitializeSDK)();

        //var functionPointer = (delegate* unmanaged[Stdcall]<IntPtr, string, string, uint, int>)function;
        //functionPointer(IntPtr.Zero, text, caption, 0);
    }


    internal static EdsError EdsTerminateSDK()
    {
        return (EdsError)((delegate*<IntPtr>)edsTerminateSDK)();
    }



#else

    static EdsNativeLib()
    {
        if (OperatingSystem.IsWindows())
        {
            string path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            string dllDir = Path.GetDirectoryName(typeof(EdsNativeLib).Assembly.Location)!;
            dllDir = Path.Combine(dllDir, LibName, Environment.Is64BitProcess ? "Win64" : "Win32");
            path = $"{path};{dllDir}";
            Environment.SetEnvironmentVariable("PATH", path);
        }
    }

    

    [LibraryImport(LibName)]
    internal static partial EdsError EdsInitializeSDK();

    [LibraryImport(LibName)]
    internal static partial EdsError EdsTerminateSDK();

    [LibraryImport(LibName)]
    internal static partial EdsError EdsRetain(IntPtr inRef);

    [LibraryImport(LibName)]
    internal static partial EdsError EdsRelease(IntPtr inRef);

    [LibraryImport(LibName)]
    internal static partial EdsError EdsGetCameraList(out IntPtr outCameraListRef);

    //[LibraryImport(LibName)]
    //internal static partial EdsError EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildCount(IntPtr inRef, out int outCount);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildAtIndex(IntPtr inRef, int inIndex, out IntPtr outRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertySize(IntPtr inRef, PropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertyData(IntPtr inRef, PropertyID inPropertyID, int inParam, int inPropertySize, IntPtr outPropertyData);

    [DllImport(LibName)]
    public extern static EdsError EdsOpenSession(IntPtr inCameraRef);

    /// <summary>
    /// Closes a logical connection with a remote camera.
    /// </summary>
    /// <param name="inCameraRef">The reference of the camera.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(LibName)]
    public extern static EdsError EdsCloseSession(IntPtr inCameraRef);

    [DllImport(LibName)]
    public extern static EdsError EdsSetCameraAddedHandler(SDKCameraAddedHandler inCameraAddedHandler, IntPtr inContext);

    [LibraryImport(LibName)]
    internal static partial EdsError EdsSendCommand(IntPtr inCameraRef, EdsCameraCommand inCommand, int inParam);

    /// <summary>
    /// Sets the remote camera state or mode.
    /// </summary>
    /// <param name="inCameraRef">The reference of the camera which will receive the command.</param>
    /// <param name="inCameraState">Specifies the command to be sent.</param>
    /// <param name="inParam">Specifies additional command-specific information.</param>
    /// <returns>Any of the SDK errors</returns>
    [LibraryImport(LibName)]
    internal static partial EdsError EdsSendStatusCommand(IntPtr inCameraRef, EdsCameraStatusCommand inCameraState, int inParam);


    [DllImport(LibName)]
    public extern static EdsError EdsGetVolumeInfo(IntPtr inCameraRef, out EdsVolumeInfo outVolumeInfo);

    /// <summary>
    /// Formats a volume.
    /// </summary>
    /// <param name="inVolumeRef">The reference of the volume.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(LibName)]
    public extern static EdsError EdsFormatVolume(IntPtr inVolumeRef);

    /// <summary>
    /// Gets information about the directory or file object on the memory card (volume) in a remote camera.
    /// </summary>
    /// <param name="inDirItemRef">The reference of the directory item.</param>
    /// <param name="outDirItemInfo">Information of the directory item.</param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(LibName)]
    public extern static EdsError EdsGetDirectoryItemInfo(IntPtr inDirItemRef, out EdsDirectoryItemInfo outDirItemInfo);

    /// <summary>
    /// Deletes a camera folder or file.
    /// If folders with subdirectories are designated, all files are deleted except protected files.
    /// DirectoryItem objects deleted by means of this method are implicitly released.
    /// Thus, there is no need to release them by means of Release.
    /// </summary>
    /// <param name="inDirItemRef"></param>
    /// <returns>Any of the SDK errors</returns>
    [DllImport(LibName)]
    public extern static EdsError EdsDeleteDirectoryItem(IntPtr inDirItemRef);
#endif
}
