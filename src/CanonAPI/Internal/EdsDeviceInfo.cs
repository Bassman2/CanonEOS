using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential)]
public struct EdsDeviceInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string szPortName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string szDeviceDescription;

    public uint DeviceSubType;

    public uint reserved;
}
