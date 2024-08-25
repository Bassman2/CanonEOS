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

        uint res = CanonSDK.EdsInitializeSDK();
        ErrorCheck(res);


        //if (IsDisposed) throw new ObjectDisposedException(nameof(CanonAPI));

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
        }
        //Release the list
        ErrorCheck(CanonSDK.EdsRelease(camlist));



    }

    public void Dispose()
    {
        
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

    private void ErrorCheck(uint errorCode)
    { 
    }
}
