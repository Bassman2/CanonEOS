using CanonAPI.Internal;
using System.Diagnostics;

namespace CanonAPI;

public sealed class Canon : IDisposable
{
    public Canon()
    {
        if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
        {
            throw new ThreadStateException("Calling thread must be in STA");
        }

        ErrorCheck(CanonSDK.EdsInitializeSDK());


        //if (IsDisposed) throw new ObjectDisposedException(nameof(CanonAPI));

        



    }

    public void Dispose()
    {
        ErrorCheck(CanonSDK.EdsTerminateSDK());
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
        ErrorCheck(CanonSDK.EdsGetCameraList(out camlist));

        //Get number of connected cameras
        int camCount;
        ErrorCheck(CanonSDK.EdsGetChildCount(camlist, out camCount));
        List<IntPtr> ptrList = new List<IntPtr>();
        for (int i = 0; i < camCount; i++)
        {
            //Get camera pointer
            IntPtr cptr;
            ErrorCheck(CanonSDK.EdsGetChildAtIndex(camlist, i, out cptr));
            ptrList.Add(cptr);

            yield return new Camera(cptr);
            // CanonSDK.EdsGetDeviceInfo(cptr, out EdsDeviceInfo Info);
        }
        //Release the list
        ErrorCheck(CanonSDK.EdsRelease(camlist));

    }

    private void ErrorCheck(uint errorCode)
    {
        if (errorCode != 0)
        { 
            Debugger.Break();
        }
    }
}
