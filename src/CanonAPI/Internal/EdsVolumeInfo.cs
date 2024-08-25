using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CanonAPI.Internal;


[StructLayout(LayoutKind.Sequential)]
public struct EdsVolumeInfo
{
    public uint StorageType;
    public uint Access;
    public ulong MaxCapacity;
    public ulong FreeSpaceInBytes;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = EdsConst.EDS_MAX_NAME)]
    public string szVolumeLabel;
}
