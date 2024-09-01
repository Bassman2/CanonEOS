using System.Reflection;

namespace CanonAPI.Internal;

internal static partial class Eds
{
    private const string LibName = "EDSDK";

    static Eds()
    {
        //if (OperatingSystem.IsWindows())
        //{
        //    string path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
        //    string dllDir = Path.GetDirectoryName(typeof(Eds).Assembly.Location)!;
        //    dllDir = Path.Combine(dllDir, LibName, Environment.Is64BitProcess ? "Win64" : "Win32");
        //    path = $"{path};{dllDir}";
        //    Environment.SetEnvironmentVariable("PATH", path);
        //}

        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == LibName)
        {
            string path = Path.Combine(Path.GetDirectoryName(assembly.Location)!, LibName, Environment.Is64BitProcess ? "Win64" : "Win32", LibName);
            return NativeLibrary.Load(path);
        }
        return IntPtr.Zero;
    }

    [DllImport(LibName)]
    public extern static EdsError EdsInitializeSDK();

    [DllImport(LibName)]
    public extern static EdsError EdsTerminateSDK();

    [DllImport(LibName)]
    public extern static EdsError EdsRetain(nint inRef);

    [DllImport(LibName)]
    public extern static EdsError EdsRelease(nint inRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildCount(nint inRef, out int outCount);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildAtIndex(nint inRef, int inIndex, out nint outRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetParent(nint inRef, out nint outParentRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetCameraList(out nint outCameraListRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetDeviceInfo(nint inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsGetVolumeInfo(nint inCameraRef, out EdsVolumeInfo outVolumeInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsGetDirectoryItemInfo(nint inDirItemRef, out EdsDirectoryItemInfo outDirItemInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsOpenSession(nint inCameraRef);

    [DllImport(LibName)]
    public extern static EdsError EdsCloseSession(nint inCameraRef);

    [DllImport(LibName)]
    public extern static EdsError EdsSendCommand(nint inCameraRef, EdsCameraCommand inCommand, int inParam);

    [DllImport(LibName)]
    public extern static EdsError EdsSendStatusCommand(nint inCameraRef, EdsCameraStatusCommand inCameraState, int inParam);

    [DllImport(LibName)]
    public extern static EdsError EdsSetCapacity(nint inCameraRef, EdsCapacity inCapacity);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertySize(nint inRef, EdsPropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertyData(nint inRef, EdsPropertyID inPropertyID, int inParam, int inPropertySize, nint outPropertyData);

    [DllImport(LibName)]
    public extern static EdsError EdsSetPropertyData(nint inRef, EdsPropertyID inPropertyID, int inParam, int inPropertySize, nint inPropertyData);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertyDesc(nint inRef, EdsPropertyID inPropertyID, out EdsPropertyDesc outPropertyDesc);

    [DllImport(LibName)]
    public extern static EdsError EdsDeleteDirectoryItem(nint inDirItemRef);

    [DllImport(LibName)]
    public extern static EdsError EdsFormatVolume(nint inVolumeRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetAttribute(nint inDirItemRef, out EdsFileAttribute outFileAttribute);

    [DllImport(LibName)]
    public extern static EdsError EdsSetAttribute(nint inDirItemRef, EdsFileAttribute inFileAttribute);

    [DllImport(LibName)]
    public extern static EdsError EdsDownload(nint inDirItemRef, int inReadSize, nint outStream);

    [DllImport(LibName)]
    public extern static EdsError EdsDownload(nint inDirItemRef, long inReadSize, nint outStream);

    [DllImport(LibName)]
    public extern static EdsError EdsDownloadComplete(nint inDirItemRef);

    [DllImport(LibName)]
    public extern static EdsError EdsDownloadCancel(nint inDirItemRef);
    
    [DllImport(LibName)]
    public extern static EdsError EdsDownloadThumbnail(nint inDirItemRef, nint outStream);
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateEvfImageRef(nint inStreamRef, out nint outEvfImageRef);
    
    [DllImport(LibName)]
    public extern static EdsError EdsDownloadEvfImage(nint inCameraRef, nint outEvfImageRef);
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateFileStream(string inFileName, EdsFileCreateDisposition inCreateDisposition, EdsFileAccess inDesiredAccess, out nint outStream);
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateFileStreamEx(string inFileName, EdsFileCreateDisposition inCreateDisposition, EdsFileAccess inDesiredAccess, out nint outStream);
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStream(int inBufferSize, out nint outStream);

    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStream(long inBufferSize, out nint outStream);


    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStreamFromPointer(nint inUserBuffer, int inBufferSize, out nint outStream);

    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStreamFromPointer(nint inUserBuffer, long inBufferSize, out nint outStream);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPointer(nint inStreamRef, out nint outPointer);
    
    [DllImport(LibName)]
    public extern static EdsError EdsRead(nint inStreamRef, int inReadSize, nint outBuffer, out int outReadSize);
    
    [DllImport(LibName)]
    public extern static EdsError EdsWrite(nint inStreamRef, int inWriteSize, nint inBuffer, out int outWrittenSize);

    [DllImport(LibName)]
    public extern static EdsError EdsSeek(nint inStreamRef, long inSeekOffset, SeekOrigin inSeekOrigin);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPosition(nint inStreamRef, out long outPosition);

    [DllImport(LibName)]
    public extern static EdsError EdsGetLength(nint inStreamRef, out long outLength);

    [DllImport(LibName)]
    public extern static EdsError EdsCopyData(nint inStreamRef, long inWriteSize, nint outStreamRef);

    [DllImport(LibName)]
    public extern static EdsError EdsCreateImageRef(nint inStreamRef, out nint outImageRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetImageInfo(nint inImageRef, EdsImageSource inImageSource, out EdsImageInfo outImageInfo);
    
    [DllImport(LibName)]
    public extern static EdsError EdsGetImage(nint inImageRef, EdsImageSource inImageSource, EdsTargetImageType inImageType, EdsRectangle inSrcRect, EdsSize inDstSize, nint outStreamRef);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetCameraAddedHandler(EdsCameraAddedHandler inCameraAddedHandler, nint inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetObjectEventHandler(nint inCameraRef, EdsObjectEventID inEvent, EdsObjectEventHandler inObjectEventHandler, nint inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetPropertyEventHandler(nint inCameraRef, EdsPropertyEventID inEvent, EdsPropertyEventHandler inPropertyEventHandler, nint inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetCameraStateEventHandler(nint inCameraRef, EdsStateEventID inEvent, EdsStateEventHandler inStateEventHandler, nint inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetProgressCallback(nint inRef, EdsProgressCallback inProgressFunc, EdsProgressOption inProgressOption, nint inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsGetEvent();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetFramePoint(nint inCameraRef, EdsFramePoint inFramePoint, bool inLockAfFrame);

    [DllImport(LibName)]
    public extern static EdsError EdsSetMetaImage(nint inDirItemRef, EdsMetaType metaType, uint inMetaDataSize, EdsMetaData metaData);
}
