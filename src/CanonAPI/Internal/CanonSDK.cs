using System.Runtime.InteropServices;

namespace CanonAPI.Internal;
internal partial class CanonSDK
{
    public static string? GetStringProperty(IntPtr inRef, PropertyID inPropertyID, int inParam = 0)
    {
        EdsDataType dt;
        int size;
        uint err = EdsGetPropertySize(inRef, inPropertyID, inParam, out dt, out size);


        IntPtr ptr = IntPtr.Zero;
        //ErrorCode err = ErrorCode.INTERNAL_ERROR;
        //outPropertyData = string.Empty;
        try
        {
            ptr = Marshal.AllocHGlobal(size);
            err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);
            return Marshal.PtrToStringAnsi(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    //public static uint GetStringProperty(IntPtr inRef, PropertyID inPropertyID, int inParam, out string outPropertyData)
    //{
    //    EdsDataType dt;
    //    int size;
    //    uint err = EdsGetPropertySize(inRef, inPropertyID, inParam, out dt, out size);

    //    IntPtr ptr = Marshal.AllocHGlobal(size);
    //    err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);

    //    outPropertyData = Marshal.PtrToStringAnsi(ptr);

    //    outPropertyData = (T)(object)"";
    //    return "";
    //}
}
