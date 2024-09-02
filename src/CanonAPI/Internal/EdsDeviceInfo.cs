namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(EdsDeviceInfoMarshaller))]
internal struct EdsDeviceInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string PortName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string DeviceDescription;

    public uint DeviceSubType;

    public uint Reserved;
}

[CustomMarshaller(typeof(EdsDeviceInfo), MarshalMode.Default, typeof(EdsDeviceInfoMarshaller))]
internal static unsafe class EdsDeviceInfoMarshaller
{
    public struct EdsDeviceInfoUnmanaged
    {
        public byte* PortName;
        public byte* DeviceDescription;
        public uint DeviceSubType;
        public uint Reserved;
    }

    public static EdsDeviceInfo ConvertToManaged(EdsDeviceInfoUnmanaged unmanaged)
    {
        return new EdsDeviceInfo
        {
            PortName = Utf8StringMarshaller.ConvertToManaged(unmanaged.PortName)!,
            DeviceDescription = Utf8StringMarshaller.ConvertToManaged(unmanaged.DeviceDescription)!,
            DeviceSubType = unmanaged.DeviceSubType,
            Reserved = unmanaged.Reserved
        };
    }

    public static EdsDeviceInfoUnmanaged ConvertToUnmanaged(EdsDeviceInfo managed)
    {
        return new EdsDeviceInfoUnmanaged
        {
            PortName = Utf8StringMarshaller.ConvertToUnmanaged(managed.PortName),
            DeviceDescription = Utf8StringMarshaller.ConvertToUnmanaged(managed.DeviceDescription),
            DeviceSubType = managed.DeviceSubType,
            Reserved = managed.Reserved
        };
    }

    public static void Free(EdsDeviceInfoUnmanaged unmanaged)
    {
        Utf8StringMarshaller.Free(unmanaged.PortName);
        Utf8StringMarshaller.Free(unmanaged.DeviceDescription);
    }
}

#if xxx
/// <summary>
/// ////////////////////////////////////////////
/// </summary>

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(Utf32StringMarshaller))]
internal static unsafe class Utf32StringMarshaller
{
    public static uint* ConvertToUnmanaged(string? managed)
        => throw new NotImplementedException();

    public static string? ConvertToManaged(uint* unmanaged)
    {
        return new DISPLAY_DEVICE
        {
            cb = unmanaged.cb,
            DeviceName = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceName),
            DeviceString = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceString),
            StateFlags = (DisplayDeviceStateFlags)unmanaged.StateFlags,
            DeviceID = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceID),
            DeviceKey = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceKey)
        };
    }
public static void Free(uint* unmanaged)
        => throw new NotImplementedException();
}

public partial class Test
{
    /*
   
    BOOL EnumDisplayDevicesA(
        [in]  LPCSTR           lpDevice,
        [in]  DWORD            iDevNum,
        [out] PDISPLAY_DEVICEA lpDisplayDevice,
        [in]  DWORD            dwFlags
    );
    typedef struct _DISPLAY_DEVICEA
    {
        DWORD cb;
        CHAR DeviceName[32];
        CHAR DeviceString[128];
        DWORD StateFlags;
        CHAR DeviceID[128];
        CHAR DeviceKey[128];
    }
    DISPLAY_DEVICEA, *PDISPLAY_DEVICEA, *LPDISPLAY_DEVICEA;
    */

    [LibraryImport("user32.dll", EntryPoint = "EnumDisplayDevicesW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool EnumDisplayDevices(string? lpDevice, uint iDevNum, [MarshalUsing(typeof(DISPLAY_DEVICEMarshaller))] ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DISPLAY_DEVICE
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string? DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string? DeviceString;
        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string? DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string? DeviceKey;

        public static DISPLAY_DEVICE Create() => new DISPLAY_DEVICE { cb = (uint)Marshal.SizeOf<DISPLAY_DEVICE>() };
    }

    [Flags]
    public enum DisplayDeviceStateFlags : uint
    {
        AttachedToDesktop = 0x1,
        MultiDriver = 0x2,
        PrimaryDevice = 0x4,
        MirroringDriver = 0x8,
        VGACompatible = 0x10,
        Removable = 0x20,
        ModesPruned = 0x8000000,
        Remote = 0x4000000,
        Disconnect = 0x2000000
    }

    [CustomMarshaller(typeof(DISPLAY_DEVICE), MarshalMode.ManagedToUnmanagedRef, typeof(DISPLAY_DEVICEMarshaller))]
    public static unsafe class DISPLAY_DEVICEMarshaller
    {
        public struct DISPLAY_DEVICE_Unmanaged
        {
            public uint cb;
            public ushort* DeviceName;
            public ushort* DeviceString;
            public uint StateFlags;
            public ushort* DeviceID;
            public ushort* DeviceKey;
        }

        public static DISPLAY_DEVICE ConvertToManaged(DISPLAY_DEVICE_Unmanaged unmanaged)
        {
            return new DISPLAY_DEVICE
            {
                cb = unmanaged.cb,
                DeviceName = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceName),
                DeviceString = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceString),
                StateFlags = (DisplayDeviceStateFlags)unmanaged.StateFlags,
                DeviceID = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceID),
                DeviceKey = Utf16StringMarshaller.ConvertToManaged(unmanaged.DeviceKey)
            };
        }

        public static DISPLAY_DEVICE_Unmanaged ConvertToUnmanaged(DISPLAY_DEVICE managed)
        {
            return new DISPLAY_DEVICE_Unmanaged
            {
                cb = managed.cb,
                DeviceName = Utf16StringMarshaller.ConvertToUnmanaged(managed.DeviceName),
                DeviceString = Utf16StringMarshaller.ConvertToUnmanaged(managed.DeviceString),
                StateFlags = (uint)managed.StateFlags,
                DeviceID = Utf16StringMarshaller.ConvertToUnmanaged(managed.DeviceID),
                DeviceKey = Utf16StringMarshaller.ConvertToUnmanaged(managed.DeviceKey)
            };
        }

        public static unsafe void Free(DISPLAY_DEVICE_Unmanaged unmanaged)
        {
            Utf16StringMarshaller.Free(unmanaged.DeviceName);
            Utf16StringMarshaller.Free(unmanaged.DeviceString);
            Utf16StringMarshaller.Free(unmanaged.DeviceID);
            Utf16StringMarshaller.Free(unmanaged.DeviceKey);
        }
    }
}
#endif