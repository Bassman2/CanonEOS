namespace CanonAPI.Internal;

internal enum EdsCameraCommand : int
{
    TakePicture = 0x00000000,
    ExtendShutDownTimer = 0x00000001,
    BulbStart = 0x00000002,
    BulbEnd = 0x00000003,
    PressShutterButton = 0x00000004,
    DoEvfAf = 0x00000102,
    DriveLensEvf = 0x00000103,
    DoClickWBEvf = 0x00000104,
}
