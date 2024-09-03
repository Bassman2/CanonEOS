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
        public fixed byte PortName[EdsConst.EDS_MAX_NAME];
        public fixed byte DeviceDescription[EdsConst.EDS_MAX_NAME];
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

    //public static EdsDeviceInfoUnmanaged ConvertToUnmanaged(EdsDeviceInfo managed)
    //{
    //    return new EdsDeviceInfoUnmanaged
    //    {
    //        //PortName = Utf8StringMarshaller.ConvertToUnmanaged(managed.PortName),
    //        //DeviceDescription = Utf8StringMarshaller.ConvertToUnmanaged(managed.DeviceDescription),
    //        DeviceSubType = managed.DeviceSubType,
    //        Reserved = managed.Reserved
    //    };
    //}

    //public static void Free(EdsDeviceInfoUnmanaged unmanaged)
    //{
    //    Utf8StringMarshaller.Free(unmanaged.PortName);
    //    Utf8StringMarshaller.Free(unmanaged.DeviceDescription);
    //}
}

