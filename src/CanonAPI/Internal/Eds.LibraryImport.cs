namespace CanonAPI.Internal;

internal static partial class EdsL
{
    private const string LibName = "EDSDK";

    static EdsL()
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

    [LibraryImport(LibName)]
    public static partial EdsError EdsInitializeSDK();

    [LibraryImport(LibName)]
    public static partial EdsError EdsTerminateSDK();

    [LibraryImport(LibName)]
    public static partial EdsError EdsRetain(nint inRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsRelease(nint inRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetChildCount(nint inRef, out int outCount);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetChildAtIndex(nint inRef, int inIndex, out nint outRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetParent(nint inRef, out nint outParentRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetCameraList(out nint outCameraListRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetDeviceInfo(nint camera, out EdsDeviceInfo deviceInfo);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetVolumeInfo(nint camera, out EdsVolumeInfo volumeInfo);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetDirectoryItemInfo(nint dirItem, out EdsDirectoryItemInfo dirItemInfo);

    [LibraryImport(LibName)]
    public static partial EdsError EdsOpenSession(nint inCameraRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCloseSession(nint inCameraRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSendCommand(nint inCameraRef, EdsCameraCommand inCommand, int inParam);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSendStatusCommand(nint inCameraRef, EdsCameraStatusCommand inCameraState, int inParam);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetCapacity(nint inCameraRef, EdsCapacity capacity);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetPropertySize(nint inRef, EdsPropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetPropertyData(nint inRef, EdsPropertyID inPropertyID, int inParam, int inPropertySize, nint outPropertyData);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetPropertyData(nint inRef, EdsPropertyID inPropertyID, int inParam, int inPropertySize, nint inPropertyData);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetPropertyDesc(nint inRef, EdsPropertyID inPropertyID, out EdsPropertyDesc outPropertyDesc);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDeleteDirectoryItem(nint inDirItemRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsFormatVolume(nint inVolumeRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetAttribute(nint inDirItemRef, out EdsFileAttribute outFileAttribute);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetAttribute(nint inDirItemRef, EdsFileAttribute inFileAttribute);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownload(nint inDirItemRef, int inReadSize, nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownload(nint inDirItemRef, long inReadSize, nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownloadComplete(nint inDirItemRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownloadCancel(nint inDirItemRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownloadThumbnail(nint inDirItemRef, nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateEvfImageRef(nint inStreamRef, out nint outEvfImageRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsDownloadEvfImage(nint inCameraRef, nint outEvfImageRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateFileStream([MarshalAs(UnmanagedType.LPStr)] string inFileName, EdsFileCreateDisposition inCreateDisposition, EdsFileAccess inDesiredAccess, out nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateFileStreamEx([MarshalAs(UnmanagedType.LPStr)] string inFileName, EdsFileCreateDisposition inCreateDisposition, EdsFileAccess inDesiredAccess, out nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateMemoryStream(int inBufferSize, out nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateMemoryStream(long inBufferSize, out nint outStream);


    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateMemoryStreamFromPointer(nint inUserBuffer, int inBufferSize, out nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateMemoryStreamFromPointer(nint inUserBuffer, long inBufferSize, out nint outStream);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetPointer(nint inStreamRef, out nint outPointer);

    [LibraryImport(LibName)]
    public static partial EdsError EdsRead(nint inStreamRef, int inReadSize, nint outBuffer, out int outReadSize);

    [LibraryImport(LibName)]
    public static partial EdsError EdsWrite(nint inStreamRef, int inWriteSize, nint inBuffer, out int outWrittenSize);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSeek(nint inStreamRef, long inSeekOffset, SeekOrigin inSeekOrigin);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetPosition(nint inStreamRef, out long outPosition);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetLength(nint inStreamRef, out long outLength);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCopyData(nint inStreamRef, long inWriteSize, nint outStreamRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsCreateImageRef(nint inStreamRef, out nint outImageRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetImageInfo(nint inImageRef, EdsImageSource inImageSource, out EdsImageInfo outImageInfo);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetImage(nint inImageRef, EdsImageSource inImageSource, EdsTargetImageType inImageType, EdsRectangle inSrcRect, EdsSize inDstSize, nint outStreamRef);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetCameraAddedHandler(EdsCameraAddedHandler inCameraAddedHandler, nint inContext);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetObjectEventHandler(nint inCameraRef, EdsObjectEventID inEvent, EdsObjectEventHandler inObjectEventHandler, nint inContext);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetPropertyEventHandler(nint inCameraRef, EdsPropertyEventID inEvent, EdsPropertyEventHandler inPropertyEventHandler, nint inContext);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetCameraStateEventHandler(nint inCameraRef, EdsStateEventID inEvent, EdsStateEventHandler inStateEventHandler, nint inContext);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetProgressCallback(nint inRef, EdsProgressCallback inProgressFunc, EdsProgressOption inProgressOption, nint inContext);

    [LibraryImport(LibName)]
    public static partial EdsError EdsGetEvent();

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetFramePoint(nint inCameraRef, EdsFramePoint inFramePoint, [MarshalAs(UnmanagedType.Bool)] bool inLockAfFrame);

    [LibraryImport(LibName)]
    public static partial EdsError EdsSetMetaImage(nint inDirItemRef, EdsMetaType metaType, uint inMetaDataSize, EdsMetaData metaData);
}
