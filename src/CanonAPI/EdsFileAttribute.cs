﻿namespace CanonAPI;

public enum EdsFileAttribute : int
{
    Normal = 0x00000000,
    ReadOnly = 0x00000001,
    Hidden = 0x00000002,
    System = 0x00000004,
    Archive = 0x00000020,
}
