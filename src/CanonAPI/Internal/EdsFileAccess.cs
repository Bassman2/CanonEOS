namespace CanonAPI.Internal;

internal enum EdsFileAccess : int
{
    Read = 0,
    Write = 1,
    ReadWrite = 2,
    Error = unchecked((int)0xFFFFFFFF),
}

