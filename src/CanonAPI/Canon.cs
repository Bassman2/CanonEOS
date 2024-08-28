namespace CanonAPI;

public sealed class Canon : IDisposable
{
    public event CameraAddedHandler? CameraAdded;
    /// <summary>
    /// The SDK camera added delegate
    /// </summary>
    private static SDKCameraAddedHandler? CameraAddedEvent;

    private uint OnCameraAddedEvent(IntPtr inContext)
    {
        ThreadPool.QueueUserWorkItem((state) => CameraAdded?.Invoke(this));
        return 0;
    }

    public Canon()
    {
        if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
        {
            throw new ThreadStateException("Calling thread must be in STA");
        }

        ErrorCheck(EdsNativeLib.EdsInitializeSDK());

        CameraAddedEvent = new SDKCameraAddedHandler(OnCameraAddedEvent);
        ErrorCheck(EdsNativeLib.EdsSetCameraAddedHandler(CameraAddedEvent, IntPtr.Zero));

        //if (IsDisposed) throw new ObjectDisposedException(nameof(CanonAPI));





    }

    public void Dispose()
    {
        ErrorCheck(EdsNativeLib.EdsTerminateSDK());
    }

    private static Version? GetSDKVersion()
    {
        var modules = Process.GetCurrentProcess().Modules;
        foreach (var module in modules)
        {
            if (module is ProcessModule pm && pm.ModuleName.ToLower() == "edsdk.dll")
            {
                FileVersionInfo vi = pm.FileVersionInfo;
                return new Version(vi.ProductMajorPart, vi.ProductMinorPart, vi.ProductBuildPart, vi.ProductPrivatePart);
            }
        }
        return null;
    }

    public IEnumerable<Camera> GetCameras()
    {
        IntPtr camlist;
        //Get camera list
        ErrorCheck(EdsNativeLib.EdsGetCameraList(out camlist));

        //Get number of connected cameras
        int camCount;
        ErrorCheck(EdsNativeLib.EdsGetChildCount(camlist, out camCount));
        List<IntPtr> ptrList = new List<IntPtr>();
        for (int i = 0; i < camCount; i++)
        {
            //Get camera pointer
            IntPtr cptr;
            ErrorCheck(EdsNativeLib.EdsGetChildAtIndex(camlist, i, out cptr));
            ptrList.Add(cptr);

            yield return new Camera(cptr);
            // CanonSDK.EdsGetDeviceInfo(cptr, out EdsDeviceInfo Info);
        }
        //Release the list
        ErrorCheck(EdsNativeLib.EdsRelease(camlist));

    }

    private void ErrorCheck(EdsError errorCode)
    {
        if (errorCode != EdsError.OK)
        { 
            Debugger.Break();
        }
    }
}
