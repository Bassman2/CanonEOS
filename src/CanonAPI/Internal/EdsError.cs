
namespace CanonAPI.Internal;

internal enum EdsError : int
{
    OK = 0x00000000,

    [Description("")]
    PROPERTIES_UNAVAILABLE = 0x00000050,
    PROPERTIES_MISMATCH = 0x00000051,
    PROPERTIES_NOT_LOADED = 0x00000053,
}