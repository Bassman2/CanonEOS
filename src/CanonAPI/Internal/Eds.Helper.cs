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

    public static IEnumerable<Property> GetProperties(IntPtr inRef)
    {
        for (EdsPropertyID propId = 0; propId < EdsPropertyID.Unknown; propId++)
        {
            EdsError err = EdsGetPropertySize(inRef, propId, 0, out EdsDataType dataType, out int size);

            if (err == 0)
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = Marshal.AllocHGlobal(size);
                    err = EdsGetPropertyData(inRef, propId, 0, size, ptr);
                    switch (dataType)
                    {
                    case EdsDataType.String:
                        string? str = Marshal.PtrToStringAnsi(ptr);
                        yield return new Property(propId, dataType, str!);
                        break;
                    case EdsDataType.Int32:
                    case EdsDataType.UInt32:
                        int int32 = Marshal.ReadInt32(ptr);
                        yield return new Property(propId, dataType, int32);
                        break;

                    default:
                        //Debug.WriteLine($"ID {propId} {dataType} {size} {err}");
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
            //else if (err != EdsError.PropertiesUnavailable)
            //{
            //    Debug.WriteLine($"ID {propId} {dt} {size} {err}");
            //}
        }
    }

    public static string? GetStringProperty(IntPtr inRef, EdsPropertyID inPropertyID, int inParam = 0)
    {
        EdsDataType dt;
        int size;
        EdsError err = EdsGetPropertySize(inRef, inPropertyID, inParam, out dt, out size);


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
