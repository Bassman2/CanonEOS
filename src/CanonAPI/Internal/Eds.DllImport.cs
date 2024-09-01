namespace CanonAPI.Internal;

internal static class EdsDllImport
{
    private const string LibName = "EDSDK";

    static EdsDllImport()
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

    [DllImport(LibName)]
    public extern static EdsError EdsInitializeSDK();

    [DllImport(LibName)]
    public extern static EdsError EdsTerminateSDK();

    [DllImport(LibName)]
    public extern static EdsError EdsRetain(IntPtr inRef);

    [DllImport(LibName)]
    public extern static EdsError EdsRelease(IntPtr inRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildCount(IntPtr inRef, out int outCount);

    [DllImport(LibName)]
    public extern static EdsError EdsGetChildAtIndex(IntPtr inRef, int inIndex, out IntPtr outRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetParent(IntPtr inRef, out IntPtr outParentRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetCameraList(out IntPtr outCameraListRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetDeviceInfo(IntPtr inCameraRef, out EdsDeviceInfo outDeviceInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsGetDirectoryItemInfo(IntPtr inDirItemRef, out EdsDirectoryItemInfo outDirItemInfo);

    [DllImport(LibName)]
    public extern static EdsError EdsOpenSession(IntPtr inCameraRef);

    [DllImport(LibName)]
    public extern static EdsError EdsCloseSession(IntPtr inCameraRef);

    [DllImport(LibName)]
    public extern static EdsError EdsSendCommand(IntPtr inCameraRef, EdsCameraCommand inCommand, int inParam);

    [DllImport(LibName)]
    public extern static EdsError EdsSendStatusCommand(IntPtr inCameraRef, EdsCameraStatusCommand inCameraState, int inParam);

    [DllImport(LibName)]
    public extern static EdsError EdsSetCapacity(IntPtr inCameraRef, EdsCapacity inCapacity);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertySize(IntPtr inRef, PropertyID inPropertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertyData(IntPtr inRef, PropertyID inPropertyID, int inParam, int inPropertySize, IntPtr outPropertyData);

    [DllImport(LibName)]
    public extern static EdsError EdsSetPropertyData(EdsBaseRef inRef,EdsPropertyID inPropertyID,EdsInt32 inParam,EdsUInt32 inPropertySize,const EdsVoid* inPropertyData )

    [DllImport(LibName)]
    public extern static EdsError EdsGetPropertyDesc();

    [DllImport(LibName)]
    public extern static EdsError EdsDeleteDirectoryItem(IntPtr inDirItemRef);

    [DllImport(LibName)]
    public extern static EdsError EdsFormatVolume(IntPtr inVolumeRef);

    [DllImport(LibName)]
    public extern static EdsError EdsGetAttribute();

    [DllImport(LibName)]
    public extern static EdsError EdsSetAttribute();

    [DllImport(LibName)]
    public extern static EdsError EdsDownload();

    [DllImport(LibName)]
    public extern static EdsError EdsDownloadComplete();

    [DllImport(LibName)]
    public extern static EdsError EdsDownloadCancel();
    
    [DllImport(LibName)]
    public extern static EdsError EdsDownloadThumbnail();
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateEvfImageRef();
    
    [DllImport(LibName)]
    public extern static EdsError EdsDownloadEvfImage();
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateFileStream();
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateFileStreamEx();
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStream();
    
    [DllImport(LibName)]
    public extern static EdsError EdsCreateMemoryStreamFromPointer();
    
    [DllImport(LibName)]
    public extern static EdsError EdsGetPointer();
    
    [DllImport(LibName)]
    public extern static EdsError EdsRead();
    
    [DllImport(LibName)]
    public extern static EdsError EdsWrite();

    [DllImport(LibName)]
    public extern static EdsError EdsSeek();

    [DllImport(LibName)]
    public extern static EdsError EdsGetPosition();
    [DllImport(LibName)]
    public extern static EdsError EdsGetLength();

    [DllImport(LibName)]
    public extern static EdsError EdsCopyData();

    [DllImport(LibName)]
    public extern static EdsError EdsCreateImageRef();

    [DllImport(LibName)]
    public extern static EdsError EdsGetImageInfo();
    
    [DllImport(LibName)]
    public extern static EdsError EdsGetImage();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetCameraAddedHandler(SDKCameraAddedHandler inCameraAddedHandler, IntPtr inContext);
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetObjectEventHandler();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetPropertyEventHandler();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetCameraStateEventHandler();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetProgressCallback();
    
    [DllImport(LibName)]
    public extern static EdsError EdsGetEvent();
    
    [DllImport(LibName)]
    public extern static EdsError EdsSetFramePoint();

    [DllImport(LibName)]
    public extern static EdsError EdsSetMetaImage();


    

    

    [DllImport(LibName)]
    public extern static EdsError EdsGetVolumeInfo(IntPtr inCameraRef, out EdsVolumeInfo outVolumeInfo);
        
   

    

    
}
