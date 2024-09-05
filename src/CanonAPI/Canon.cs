namespace CanonAPI;

public delegate void CameraAddedEventHandler(Canon sender);

public sealed class Canon : IDisposable
{
    public event CameraAddedEventHandler? CameraAdded;
        
    private static EdsCameraAddedHandler? EdsCameraAddedEvent;

    private EdsError OnCameraAddedEvent(nint inContext)
    {
        ThreadPool.QueueUserWorkItem((state) => CameraAdded?.Invoke(this));
        return 0;
    }

    //private static EdsObjectEventHandler? (EdsObjectEventID inEvent, IntPtr inRef, IntPtr inContext);
    

    public Canon()
    {
        if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
        {
            throw new ThreadStateException("Calling thread must be in STA");
        }

        Eds.CheckError(Eds.EdsInitializeSDK());

        EdsCameraAddedEvent = new EdsCameraAddedHandler(OnCameraAddedEvent);
        Eds.CheckError(Eds.EdsSetCameraAddedHandler(EdsCameraAddedEvent, nint.Zero));

        
        //Eds.CheckError(Eds.EdsSetCameraAddedHandler(new EdsCameraAddedHandler(OnCameraAddedEvent), nint.Zero));

        IsInitialized = true;
        //if (IsDisposed) throw new ObjectDisposedException(nameof(CanonAPI));
    }

    public static Version EdsdkFileVersion { get; } = Eds.FileVersion;
    public static Version EdsdkProductVersion { get; } = Eds.ProductVersion;
    public static string EdsdkPath { get; } = Eds.LibraryPath;

    public bool IsInitialized { get; private set; }

    public void Dispose()
    {
        Eds.CheckError(Eds.EdsTerminateSDK());
        IsInitialized = false;
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
        Eds.CheckError(Eds.EdsGetCameraList(out camlist));

        //Get number of connected cameras
        int camCount;
        Eds.CheckError(Eds.EdsGetChildCount(camlist, out camCount));
        List<IntPtr> ptrList = new List<IntPtr>();
        for (int i = 0; i < camCount; i++)
        {
            //Get camera pointer
            IntPtr cptr;
            Eds.CheckError(Eds.EdsGetChildAtIndex(camlist, i, out cptr));
            ptrList.Add(cptr);

            yield return new Camera(cptr);
            // CanonSDK.EdsGetDeviceInfo(cptr, out EdsDeviceInfo Info);
        }
        //Release the list
        Eds.CheckError(Eds.EdsRelease(camlist));

    }
}
