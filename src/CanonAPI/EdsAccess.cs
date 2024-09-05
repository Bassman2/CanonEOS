namespace CanonAPI;

public enum EdsAccess : uint
{
    
    ReadOnly = 0,
    WriteOnly = 1,
    ReadWrite = 2,
    AccessError = 0xFFFFFFFF
}
