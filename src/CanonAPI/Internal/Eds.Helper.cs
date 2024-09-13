using System.Reflection.PortableExecutable;

namespace CanonAPI.Internal;


// https://developers.canon-europe.com/s/camera
// https://developers.canon-europe.com/developers/s/camera-downloads

internal static partial class Eds
{
    private const string LibName = "EDSDK";

    private static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
        //return Assembly.GetAssembly(typeof(Eds))!;
    }

    private static string GetAssemblyFolder()
    {
        return Path.GetDirectoryName(GetAssembly().Location)!;
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == LibName)
        {
            //string path = Path.Combine(Path.GetDirectoryName(assembly.Location)!, LibName, Environment.Is64BitProcess ? "Win64" : "Win32", LibName);
            //string path = GetLibraryFile();
            return NativeLibrary.Load(LibraryPath);
        }
        return IntPtr.Zero;
    }

    //private static string GetLibraryFolder()
    //{
    //    string appfolder = GetAssemblyFolder();
    //    return Path.Combine(appfolder, LibName, Environment.Is64BitProcess ? "Win64" : "Win32");
    //}

    private static string LibraryExtention()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return ".dll";
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return ".dmg";
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return ".so";
        }
        throw new NotSupportedException();
    }

    private static string GetLibraryFile()
    {
        //Assembly assembly = Assembly.GetAssembly(typeof(Eds))!;
        //string appfolder = Path.GetDirectoryName(assembly.Location)!;
        string appfolder = GetAssemblyFolder();
        return Path.Combine(appfolder, LibName, Environment.Is64BitProcess ? "Win64" : "Win32", LibName);
    }

    private static Version GetEdsFileVersion()
    {
        string file = GetLibraryFile();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(file + LibraryExtention());
        return new Version(fileVersionInfo.FileVersion!);
    }

    private static Version GetEdsProductVersion()
    {
        string file = GetLibraryFile();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(file + LibraryExtention());
        return new Version(fileVersionInfo.ProductVersion!);
    }

    public static Version FileVersion { get; } = GetEdsFileVersion();

    public static Version ProductVersion { get; } = GetEdsProductVersion();

    public static string LibraryPath { get; } = GetLibraryFile();

    /*
    public static void DebugProperties(IntPtr inRef)
    {
        for (EdsPropertyID propId = 0; propId < EdsPropertyID.Unknown; propId++)
        {
            EdsError err = EdsGetPropertySize(inRef, (EdsPropertyID)propId, 0, out EdsDataType dt, out int size);

            if (err == 0)
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.AllocHGlobal(size);
                    err = EdsGetPropertyData(inRef, propId, 0, size, ptr);
                    switch (dt)
                    {
                    case EdsDataType.String:
                        string? str = Marshal.PtrToStringAnsi(ptr);
                        Debug.WriteLine($"ID {propId} {dt} {size} {err} \"{str}\"");
                        break;
                    case EdsDataType.Int32:
                    case EdsDataType.UInt32:
                        int int32 = Marshal.ReadInt32(ptr);
                        Debug.WriteLine($"ID {propId} {dt} {size} {err} \"{int32}\"");
                        break;


                    default:
                        Debug.WriteLine($"ID {propId} {dt} {size} {err}");
                        break;
                    }
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(ptr);
                    }
                }
            }
            else if (err != EdsError.PropertiesUnavailable)
            {
                Debug.WriteLine($"ID {propId} {dt} {size} {err}");
            }
        }
    }
    */

    public static bool IsMultiParam(EdsPropertyID propertyID)
    {
        var x = typeof(EdsPropertyID).GetField(propertyID.ToString());
        var y = x?.IsDefined(typeof(MultiParamAttribute), false);
        return typeof(EdsPropertyID).GetField(propertyID.ToString())?.IsDefined(typeof(MultiParamAttribute), false) ?? false;
    }

    public static IEnumerable<PropertyDesc> GetPropertiesDesc(IntPtr camera)
    {
        for (EdsPropertyID propertyID = EdsPropertyID.CameraFrom; propertyID < EdsPropertyID.AtCaptureTo; propertyID++)
        {
            EdsGetPropertyDesc(camera, propertyID, out EdsPropertyDesc propertyDesc);

            yield return new PropertyDesc(propertyID, propertyDesc);
        }
    }

    //private static bool ParamFix(EdsPropertyID propertyID, int param) => !((propertyID == EdsPropertyID.DateTime || propertyID == EdsPropertyID.AvailableShots || propertyID == (EdsPropertyID)96) && param > 0);
    


    private static IEnumerable<Property> GetAllProperties(IntPtr inRef)
    {
        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.PropertyFrom, EdsPropertyID.PropertyTo))
        {
            yield return property;
        }
        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.LimitedFrom, EdsPropertyID.LimitedTo))
        {
            yield return property;
        }
        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.AtCaptureFrom, EdsPropertyID.AtCaptureTo))
        {
            yield return property;
        }
    }

    public static IEnumerable<Property> GetCameraProperties(IntPtr inRef) => GetAllProperties(inRef);
    public static IEnumerable<Property> GetPictureProperties(IntPtr inRef) => GetAllProperties(inRef);

    /*
    public static IEnumerable<Property> GetCameraProperties(IntPtr inRef)
    {
        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.CameraFrom, EdsPropertyID.CameraTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.ImageFrom, EdsPropertyID.ImageTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.ProcessingFrom, EdsPropertyID.ProcessingTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.CaptureFrom, EdsPropertyID.CaptureTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.EvfFrom, EdsPropertyID.EvfTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.PowerShotFrom, EdsPropertyID.PowerShotTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.GpsFrom, EdsPropertyID.GpsTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.LimitedFrom, EdsPropertyID.LimitedTo))
        {
            yield return property;
        }

        foreach (Property property in GetPropertiesRange(inRef, EdsPropertyID.AtCaptureFrom, EdsPropertyID.AtCaptureTo))
        {
            yield return property;
        }

        

        for (EdsPropertyID propertyID = EdsPropertyID.CameraFrom; propertyID < EdsPropertyID.CameraTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
        for (EdsPropertyID propertyID = EdsPropertyID.LimitedFrom; propertyID < EdsPropertyID.LimitedTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
        for (EdsPropertyID propertyID = EdsPropertyID.AtCaptureFrom; propertyID < EdsPropertyID.AtCaptureTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
        
    }

    public static IEnumerable<Property> GetPictureProperties(IntPtr inRef)
    {

        for (EdsPropertyID propertyID = EdsPropertyID.PropertyFrom; propertyID < EdsPropertyID.PropertyTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
        for (EdsPropertyID propertyID = EdsPropertyID.LimitedFrom; propertyID < EdsPropertyID.LimitedTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
        for (EdsPropertyID propertyID = EdsPropertyID.AtCaptureFrom; propertyID < EdsPropertyID.AtCaptureTo; propertyID++)
        {
            var properties = GetProperties(inRef, propertyID);
            foreach (var property in properties)
            {
                yield return property;
            }
        }
    }
    */

    private static IEnumerable<Property> GetPropertiesRange(IntPtr inRef, EdsPropertyID from, EdsPropertyID to)
    {
        for (EdsPropertyID propertyID = from; propertyID <= to; propertyID++)
        {
            if (IsMultiParam(propertyID))
            {
                var properties = GetMultiParamProperty(inRef, propertyID);
                foreach (var property in properties.Where(p => p != null))
                {
                    yield return property;
                }
            }
            else
            {
                Property? property = GetSingelParamProperty(inRef, propertyID);
                if (property != null)
                { 
                    yield return property;
                }
            }
        }
    }

    private static Property? GetSingelParamProperty(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        EdsError err;
        if ((err = EdsGetPropertySize(inRef, propertyID, 0, out EdsDataType dataType, out int size)) == EdsError.OK)
        {
            return new Property(propertyID, param, dataType,
                dataType switch
                {
                    EdsDataType.String => GetPropertyString(inRef, propertyID, param),
                    EdsDataType.Int32 => GetPropertyInt(inRef, propertyID, param),
                    EdsDataType.UInt32 => GetPropertyUInt(inRef, propertyID, param),
                    EdsDataType.Time => (DateTime?)GetPropertyStruct<EdsTime>(inRef, propertyID, param),
                    EdsDataType.Int32_Array => GetPropertyIntArray(inRef, propertyID, param),
                    EdsDataType.UInt32_Array => GetPropertyUIntArray(inRef, propertyID),
                    EdsDataType.FocusInfo => GetPropertyStruct<EdsFocusInfo>(inRef, propertyID, param),
                    EdsDataType.PictureStyleDesc => GetPropertyStruct<EdsPictureStyleDesc>(inRef, propertyID, param),
                    _ => new NotSupportedException()
                });
        }
        /*
            //Debug.WriteLine($"Property {propertyID} {param} {size} {err}");
            switch (dataType)
            {
            case EdsDataType.String:
                return new Property(propertyID, 0, dataType, GetPropertyString(inRef, propertyID));
            case EdsDataType.Int32:
                return new Property(propertyID, 0, dataType, GetPropertyInt(inRef, propertyID));
            case EdsDataType.UInt32:
                return new Property(propertyID, 0, dataType, GetPropertyUInt(inRef, propertyID));
            case EdsDataType.Time:
               return new Property(propertyID, param, dataType, (DateTime?)GetPropertyStruct<EdsTime>(inRef, propertyID));
                break;
            case EdsDataType.Int32_Array:
                yield return new Property(propertyID, param, dataType, GetPropertyIntArray(inRef, propertyID));
                break;
            case EdsDataType.UInt32_Array:
                yield return new Property(propertyID, param, dataType, GetPropertyUIntArray(inRef, propertyID));
                break;
            case EdsDataType.FocusInfo:
                yield return new Property(propertyID, param, dataType, GetPropertyStruct<EdsFocusInfo>(inRef, propertyID));
                break;
            case EdsDataType.PictureStyleDesc:
                yield return new Property(propertyID, param, dataType, GetPropertyStruct<EdsPictureStyleDesc>(inRef, propertyID));
                break;

            case EdsDataType.ByteBlock:
                break;
            default:
                break;
            }
            param++;
        }
        */
        return null;
    }

    private static IEnumerable<Property> GetMultiParamProperty(IntPtr inRef, EdsPropertyID propertyID)
    {
        for (int param = 0; param < 100; param++)
        {
            Property? property = GetSingelParamProperty(inRef, propertyID, param);
            if (property is not null)
            {
                yield return property;
            }
            else
            {
                yield break;
            }
        }
        /*
        EdsError err;
        int param = 0;
        while ((err = EdsGetPropertySize(inRef, propertyID, param, out EdsDataType dataType, out int size)) == EdsError.OK && param < 10)
        {
            //Debug.WriteLine($"Property {propertyID} {param} {size} {err}");
            switch (dataType)
            {
            case EdsDataType.String:
                yield return new Property(propertyID, param, dataType, GetPropertyString(inRef, propertyID));
                break;
            case EdsDataType.Int32:
                yield return new Property(propertyID, param, dataType, GetPropertyInt(inRef, propertyID));
                break;
            case EdsDataType.UInt32:
                yield return new Property(propertyID, param, dataType, GetPropertyUInt(inRef, propertyID));
                break;
            case EdsDataType.Time:
                yield return new Property(propertyID, param, dataType, (DateTime?)GetPropertyStruct<EdsTime>(inRef, propertyID));
                break;
            case EdsDataType.Int32_Array:
                yield return new Property(propertyID, param, dataType, GetPropertyIntArray(inRef, propertyID));
                break;
            case EdsDataType.UInt32_Array:
                yield return new Property(propertyID, param, dataType, GetPropertyUIntArray(inRef, propertyID));
                break;
            case EdsDataType.FocusInfo:
                yield return new Property(propertyID, param, dataType, GetPropertyStruct<EdsFocusInfo>(inRef, propertyID));
                break;
            case EdsDataType.PictureStyleDesc:
                yield return new Property(propertyID, param, dataType, GetPropertyStruct<EdsPictureStyleDesc>(inRef, propertyID));
                break;

            case EdsDataType.ByteBlock:
                break;
            default:
                break;
            }
            param++;
        }
        */
    }

   

    public unsafe static int GetPropertyInt(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        int value = 0;
        int* ptr = &value;
        EdsGetPropertyData(inRef, propertyID, param, sizeof(int), (IntPtr)ptr);
        return value;
    }

    public unsafe static uint GetPropertyUInt(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        uint value = 0;
        uint* ptr = &value;
        EdsGetPropertyData(inRef, propertyID, param, sizeof(uint), (IntPtr)ptr);
        return value;
    }

    public unsafe static int[] GetPropertyIntArray(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        EdsGetPropertySize(inRef, propertyID, param, out EdsDataType dataType, out int size);

        if (size == 0)
        {
            return [];
        }

        int[] array = new int[size / sizeof(int)];
        fixed (int* ptr = &array[0])
        {   
            EdsGetPropertyData(inRef, propertyID, param, size, (IntPtr)ptr);
        }
        return array;
    }

    public unsafe static uint[] GetPropertyUIntArray(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        Eds.CheckError(EdsGetPropertySize(inRef, propertyID, param, out EdsDataType dataType, out int size));

        if (size == 0)
        {
            return [];
        }

        uint[] array = new uint[size / sizeof(uint)];
        fixed (uint* ptr = &array[0])
        {    
            EdsGetPropertyData(inRef, propertyID, param, size, (IntPtr)ptr);
        }
        return array;
    }


    public static string? GetPropertyString(IntPtr inRef, EdsPropertyID propertyID, int inParam = 0)
    {
        EdsGetPropertySize(inRef, propertyID, inParam, out EdsDataType dataType, out int size);

        nint ptr = nint.Zero;
        try
        {
            ptr = Marshal.AllocHGlobal(size);
            EdsGetPropertyData(inRef, propertyID, inParam, size, ptr);
            return Marshal.PtrToStringAnsi(ptr);
        }
        finally
        {
            if (ptr != nint.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    public unsafe static T? GetPropertyStruct<T>(IntPtr inRef, EdsPropertyID propertyID, int param = 0)
    {
        nint ptr = nint.Zero;
        try
        {
            int size = Marshal.SizeOf(typeof(T));
            ptr = Marshal.AllocHGlobal(size);
            EdsGetPropertyData(inRef, propertyID, param, size, ptr);
            T? value = Marshal.PtrToStructure<T>(ptr);
            return value;
        }
        finally
        {
            if (ptr != nint.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }


    public static IEnumerable<nint> GetChildren(nint item)
    {
        EdsGetChildCount(item, out int count);
        for (int i = 0; i < count; i++)
        {
            EdsGetChildAtIndex(item, i, out nint child);
            yield return child;
        }
    }

    public static EdsError CheckError(EdsError error)
    {
        if (error != EdsError.OK)
        {
            Debug.WriteLine($"Error: {error}!");
            Debugger.Break();
        }
        return error;
    }
}
