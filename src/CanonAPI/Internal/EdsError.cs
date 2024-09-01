
namespace CanonAPI.Internal;

internal enum EdsError : int
{
    OK = 0x00000000,



    // Miscellaneous errors

    Unimplemented = 0x00000001,
    InternalError = 0x00000002,
    MemAllocFailed = 0x00000003,
    MemFreeFailed = 0x00000004,
    OperationCancelled = 0x00000005,
    IncompatibleVersion = 0x00000006,
    NotSupported = 0x00000007,
    UnexpectedException = 0x00000008,
    ProtectionViolation = 0x00000009,
    MissingSubcomponent = 0x0000000A,
    SelectionUnavailable = 0x0000000B,


    [Description("")]
    PropertiesUnavailable = 0x00000050,
    PropertiesMismatch = 0x00000051,
    PropertiesNotLoaded = 0x00000053,
}