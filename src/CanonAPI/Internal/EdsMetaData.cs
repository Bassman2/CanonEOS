using System.Runtime.InteropServices.Marshalling;

namespace CanonAPI.Internal;

[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(EdsMetaDataMarshaller))]
internal struct EdsMetaData
{
    public byte LatitudeRef;
    public byte LongitudeRef;
    public byte AltitudeRef;
    public byte Status;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public EdsRational[] Latitude;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public EdsRational[] Longitude;
}

[CustomMarshaller(typeof(EdsMetaData), MarshalMode.Default, typeof(EdsMetaDataMarshaller))]
internal static unsafe class EdsMetaDataMarshaller
{
    public struct EdsMetaDataUnmanaged
    {
        public byte LatitudeRef;
        public byte LongitudeRef;
        public byte AltitudeRef;
        public byte Status;
        public EdsRational* Latitude;
        public EdsRational* Longitude;
    }

    public static EdsMetaData ConvertToManaged(EdsMetaDataUnmanaged unmanaged)
    {
        return new EdsMetaData
        {
            LatitudeRef = unmanaged.LatitudeRef,
            LongitudeRef = unmanaged.LongitudeRef,
            AltitudeRef = unmanaged.AltitudeRef,
            Status = unmanaged.Status,
            Latitude = ArrayMarshaller<EdsRational, EdsRational>.AllocateContainerForManagedElements(unmanaged.Latitude, 3)!,
            Longitude = ArrayMarshaller<EdsRational, EdsRational>.AllocateContainerForManagedElements(unmanaged.Longitude, 3)!

        };
    }

    public static EdsMetaDataUnmanaged ConvertToUnmanaged(EdsMetaData managed)
    {
        return new EdsMetaDataUnmanaged
        {
            LatitudeRef = managed.LatitudeRef,
            LongitudeRef = managed.LongitudeRef,
            AltitudeRef = managed.AltitudeRef,
            Status = managed.Status,
            Latitude = ArrayMarshaller<EdsRational, EdsRational>.AllocateContainerForUnmanagedElements(managed.Latitude, out int _),
            Longitude = ArrayMarshaller<EdsRational, EdsRational>.AllocateContainerForUnmanagedElements(managed.Longitude, out int _)

        };
    }

    public static void Free(EdsMetaDataUnmanaged unmanaged)
    {
        ArrayMarshaller<EdsRational, EdsRational>.Free(unmanaged.Latitude);
        ArrayMarshaller<EdsRational, EdsRational>.Free(unmanaged.Longitude);
    }
}
