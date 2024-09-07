﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

    private static bool ParamFix(EdsPropertyID propertyID, int param) => !((propertyID == EdsPropertyID.DateTime || propertyID == EdsPropertyID.AvailableShots || propertyID == (EdsPropertyID)96) && param > 0);

    public static IEnumerable<Property> GetProperties(IntPtr inRef)
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

    private static IEnumerable<Property> GetProperties(IntPtr inRef, EdsPropertyID propertyID)
    {
        EdsError err;
        int param = 0;
        while ((err = EdsGetPropertySize(inRef, propertyID, param, out EdsDataType dataType, out int size)) == EdsError.OK && param < 10 && ParamFix(propertyID, param))
        {
            Debug.WriteLine($"Property {propertyID} {param} {size} {err}");
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
        Debug.Unindent();
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
