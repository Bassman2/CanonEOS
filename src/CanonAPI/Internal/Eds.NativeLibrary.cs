using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CanonAPI.Internal;

internal static partial class Eds
{
    private const string LibName = "EDSDK";

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError InitializeSDK();

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError TerminateSDK();

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError Retain(nint item);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError Release(nint item);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetChildCount(nint item, out int outCount);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetChildAtIndex(nint item, int inIndex, out nint outRef);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetParent(nint item, out nint outParentRef);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetCameraList(out nint list);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetDeviceInfo(nint item, out nint deviceInfo);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetVolumeInfo(nint camera, out nint volumeInfo);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetDirectoryItemInfo(nint directoryItem, out nint directoryItemInfo);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError OpenSession(nint camera);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError CloseSession(nint camera);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetPropertySize(nint item, EdsPropertyID propertyID, int inParam, out EdsDataType outDataType, out int outSize);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError GetPropertyData(nint item, EdsPropertyID propertyID, int inParam, int inPropertySize, nint outPropertyData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError SetPropertyData(nint item, EdsPropertyID propertyID, int inParam, int inPropertySize, nint inPropertyData);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    delegate EdsError SetCameraAddedHandler(nint func, nint context);

        


    private static readonly nint library;


    private static readonly InitializeSDK initializeSDK;
    private static readonly TerminateSDK terminateSDK;
    private static readonly Retain retain;
    private static readonly Release release;
    private static readonly GetChildCount getChildCount;
    private static readonly GetChildAtIndex getChildAtIndex;
    private static readonly GetParent getParent;
    private static readonly GetCameraList getCameraList;
    private static readonly GetDeviceInfo getDeviceInfo;
    private static readonly GetVolumeInfo getVolumeInfo;
    private static readonly GetDirectoryItemInfo getDirectoryItemInfo;
    private static readonly OpenSession openSession;
    private static readonly CloseSession closeSession;

    private static readonly GetPropertySize getPropertySize;
    private static readonly GetPropertyData getPropertyData;
    private static readonly SetPropertyData setPropertyData;

    private static SetCameraAddedHandler setCameraAddedHandler;

    static Eds()
    {
        string path = Path.Combine(AppContext.BaseDirectory, LibName, Environment.Is64BitProcess ? "Win64" : "Win32", LibName);
        library = NativeLibrary.Load(path);

        nint addr = NativeLibrary.GetExport(library, nameof(EdsInitializeSDK));
        initializeSDK = Marshal.GetDelegateForFunctionPointer<InitializeSDK>(addr);

        addr = NativeLibrary.GetExport(library, nameof(EdsTerminateSDK));
        terminateSDK = Marshal.GetDelegateForFunctionPointer<TerminateSDK>(addr);

        retain = Marshal.GetDelegateForFunctionPointer<Retain>(NativeLibrary.GetExport(library, nameof(EdsRetain)));
        release = Marshal.GetDelegateForFunctionPointer<Release>(NativeLibrary.GetExport(library, nameof(EdsRelease)));

        getChildCount = Marshal.GetDelegateForFunctionPointer<GetChildCount>(NativeLibrary.GetExport(library, nameof(EdsGetChildCount)));
        getChildAtIndex = Marshal.GetDelegateForFunctionPointer<GetChildAtIndex>(NativeLibrary.GetExport(library, nameof(EdsGetChildAtIndex)));
        getParent = Marshal.GetDelegateForFunctionPointer<GetParent>(NativeLibrary.GetExport(library, nameof(EdsGetParent)));

        getCameraList = Marshal.GetDelegateForFunctionPointer<GetCameraList>(NativeLibrary.GetExport(library, nameof(EdsGetCameraList)));
        getDeviceInfo = Marshal.GetDelegateForFunctionPointer<GetDeviceInfo>(NativeLibrary.GetExport(library, nameof(EdsGetDeviceInfo)));

        getVolumeInfo = Marshal.GetDelegateForFunctionPointer<GetVolumeInfo>(NativeLibrary.GetExport(library, nameof(EdsGetVolumeInfo)));
        getDirectoryItemInfo = Marshal.GetDelegateForFunctionPointer<GetDirectoryItemInfo>(NativeLibrary.GetExport(library, nameof(EdsGetDirectoryItemInfo)));
        openSession = Marshal.GetDelegateForFunctionPointer<OpenSession>(NativeLibrary.GetExport(library, nameof(EdsOpenSession)));
        closeSession = Marshal.GetDelegateForFunctionPointer<CloseSession>(NativeLibrary.GetExport(library, nameof(EdsCloseSession)));

        getPropertySize = Marshal.GetDelegateForFunctionPointer<GetPropertySize>(NativeLibrary.GetExport(library, nameof(EdsGetPropertySize)));
        getPropertyData = Marshal.GetDelegateForFunctionPointer<GetPropertyData>(NativeLibrary.GetExport(library, nameof(EdsGetPropertyData)));
        setPropertyData = Marshal.GetDelegateForFunctionPointer<SetPropertyData>(NativeLibrary.GetExport(library, nameof(EdsSetPropertyData)));

        setCameraAddedHandler = Marshal.GetDelegateForFunctionPointer<SetCameraAddedHandler>(NativeLibrary.GetExport(library, nameof(EdsSetCameraAddedHandler)));
    }

    internal static EdsError EdsInitializeSDK()
    {
        return initializeSDK();
    }


    internal static EdsError EdsTerminateSDK()
    {
        return terminateSDK();
    }

    internal static EdsError EdsRetain(nint item) => retain(item);

    internal static EdsError EdsRelease(nint item) => release(item);

    internal static EdsError EdsGetChildCount(nint item, out int count) => getChildCount(item, out count);

    internal static EdsError EdsGetChildAtIndex(nint item, int index, out nint child) => getChildAtIndex(item, index, out child);

    internal static EdsError EdsGetParent(nint item, out nint parent) => getParent(item, out parent);

    internal static EdsError EdsGetCameraList(out nint list) => getCameraList(out list);

    internal static EdsError EdsGetDeviceInfo(nint camera, out EdsDeviceInfo deviceInfo)
    {
        EdsError err = getDeviceInfo(camera, out nint refDeviceInfo);
        deviceInfo = err == EdsError.OK ? Marshal.PtrToStructure<EdsDeviceInfo>(refDeviceInfo) : new EdsDeviceInfo();
        return err;
    }

    internal static EdsError EdsGetVolumeInfo(nint camera, out EdsVolumeInfo volumeInfo)
    {
        EdsError err = getVolumeInfo(camera, out nint refVolumeInfo);
        volumeInfo = err == EdsError.OK ? Marshal.PtrToStructure<EdsVolumeInfo>(refVolumeInfo) : new EdsVolumeInfo();
        return err;
    }

    internal static EdsError EdsGetDirectoryItemInfo(nint directoryItem, out EdsDirectoryItemInfo directoryItemInfo)
    {
        EdsError err = getDirectoryItemInfo(directoryItem, out nint refdirectoryItemInfo);
        directoryItemInfo = err == EdsError.OK ? Marshal.PtrToStructure<EdsDirectoryItemInfo>(refdirectoryItemInfo) : new EdsDirectoryItemInfo();
        return err;
    }

    internal static EdsError EdsOpenSession(nint camera) => openSession(camera);

    internal static EdsError EdsCloseSession(nint camera) => closeSession(camera);


    internal static EdsError EdsGetPropertySize(nint item, EdsPropertyID propertyID, int param, out EdsDataType dataType, out int size)
    {
        return getPropertySize(item, propertyID, param, out dataType, out size);
    }

    internal static EdsError EdsGetPropertyData(nint item, EdsPropertyID propertyID, int param, int size, nint propertyData)
    {
        return getPropertyData(item, propertyID, param, size, propertyData);
    }

    internal static EdsError EdsSetPropertyData(nint item, EdsPropertyID propertyID, int param, int size, nint propertyData)
    {
        return setPropertyData(item, propertyID, param, size, propertyData);
    }



    internal static EdsError EdsSetCameraAddedHandler(EdsCameraAddedHandler cameraAddedHandler, nint context) 
    {
        nint func = Marshal.GetFunctionPointerForDelegate(cameraAddedHandler);
        return setCameraAddedHandler(func, context);
    }
}
