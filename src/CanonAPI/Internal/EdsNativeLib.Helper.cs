//namespace CanonAPI.Internal;

//internal partial class EdsNativeLib
//{
//    public static void DebugProperties(IntPtr inRef)
//    {
//        for (PropertyID propId = 0; propId < PropertyID.Unknown; propId++)
//        {
//            EdsError err = EdsGetPropertySize(inRef, (PropertyID)propId, 0, out EdsDataType dt, out int size);

//            if (err == 0)
//            {
//                IntPtr ptr = IntPtr.Zero;                
//                try
//                {
//                    ptr = Marshal.AllocHGlobal(size);
//                    err = EdsGetPropertyData(inRef, propId, 0, size, ptr);
//                    switch (dt)
//                    {
//                    case EdsDataType.String:
//                        string? str = Marshal.PtrToStringAnsi(ptr);
//                        Debug.WriteLine($"ID {propId} {dt} {size} {err} \"{str}\"");
//                        break;
//                    case EdsDataType.Int32:
//                    case EdsDataType.UInt32:
//                        int int32 = Marshal.ReadInt32(ptr);
//                        Debug.WriteLine($"ID {propId} {dt} {size} {err} \"{int32}\"");
//                        break;
                    
                       
//                    default:
//                        Debug.WriteLine($"ID {propId} {dt} {size} {err}");
//                        break;
//                    }
//                }
//                finally
//                {
//                    if (ptr != IntPtr.Zero)
//                    {
//                        Marshal.FreeHGlobal(ptr);
//                    }
//                }   
//            }
//            else if (err != EdsError.PROPERTIES_UNAVAILABLE)
//            {
//                Debug.WriteLine($"ID {propId} {dt} {size} {err}");
//            }
//        }
//    }
//    public static string? GetStringProperty(IntPtr inRef, PropertyID inPropertyID, int inParam = 0)
//    {
//        EdsDataType dt;
//        int size;
//        EdsError err = EdsGetPropertySize(inRef, inPropertyID, inParam, out dt, out size);


//        IntPtr ptr = IntPtr.Zero;
//        //ErrorCode err = ErrorCode.INTERNAL_ERROR;
//        //outPropertyData = string.Empty;
//        try
//        {
//            ptr = Marshal.AllocHGlobal(size);
//            err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);
//            return Marshal.PtrToStringAnsi(ptr);
//        }
//        finally
//        {
//            if (ptr != IntPtr.Zero)
//            {
//                Marshal.FreeHGlobal(ptr);
//            }
//        }
//    }

//    //public static uint GetStringProperty(IntPtr inRef, PropertyID inPropertyID, int inParam, out string outPropertyData)
//    //{
//    //    EdsDataType dt;
//    //    int size;
//    //    uint err = EdsGetPropertySize(inRef, inPropertyID, inParam, out dt, out size);

//    //    IntPtr ptr = Marshal.AllocHGlobal(size);
//    //    err = EdsGetPropertyData(inRef, inPropertyID, inParam, size, ptr);

//    //    outPropertyData = Marshal.PtrToStringAnsi(ptr);

//    //    outPropertyData = (T)(object)"";
//    //    return "";
//    //}

//    public static IEnumerable<nint> GetChildren(nint item)
//    {
//        EdsGetChildCount(item, out int count);
//        for (int i = 0; i < count; i++)
//        {
//            EdsGetChildAtIndex(item, i, out nint child);
//            yield return child;
//        }
//    }
//}
