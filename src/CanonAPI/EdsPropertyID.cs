﻿namespace CanonAPI;

public enum EdsPropertyID : uint
{
    Unknown = 0x0000FFFF,

    [Description("Name of the camera")]
    ProductName = 0x00000002,

    [Description("Owner name for the camera")]
    OwnerName = 0x00000004,
    
    [Description("Manufacturer")]
    MakerName = 0x00000005,

    [Description("System time of the camera")]
    DateTime = 0x00000006,

    [Description("Firmware version of the camera")]
    FirmwareVersion = 0x00000007,

    [Description("Battery level of the camera")]
    BatteryLevel = 0x00000008,

    CFn = 0x00000009,
    SaveTo = 0x0000000b,
    CurrentStorage = 0x0000000c,
    CurrentFolder = 0x0000000d,
    MyMenu = 0x0000000e,

    [Description("Level of degradation of the battery")] 
    BatteryQuality = 0x00000010,

    [Description("Serial number of the camera")]
    BodyIDEx = 0x00000015,

    HDDirectoryStructure = 0x00000020,

    //Image Properties
    ImageQuality = 0x00000100,
    JpegQuality = 0x00000101,
    Orientation = 0x00000102,
    ICCProfile = 0x00000103,
    FocusInfo = 0x00000104,
    DigitalExposure = 0x00000105,
    WhiteBalance = 0x00000106,
    ColorTemperature = 0x00000107,
    WhiteBalanceShift = 0x00000108,
    Contrast = 0x00000109,
    ColorSaturation = 0x0000010a,
    ColorTone = 0x0000010b,
    Sharpness = 0x0000010c,
    ColorSpace = 0x0000010d,
    ToneCurve = 0x0000010e,
    PhotoEffect = 0x0000010f,
    FilterEffect = 0x00000110,
    ToningEffect = 0x00000111,
    ParameterSet = 0x00000112,
    ColorMatrix = 0x00000113,
    PictureStyle = 0x00000114,
    PictureStyleDesc = 0x00000115,
    PictureStyleCaption = 0x00000200,

    //Image Processing Properties
    Linear = 0x00000300,
    ClickWBPoint = 0x00000301,
    WBCoeffs = 0x00000302,

    //Image GPS Properties
    GPSVersionID = 0x00000800,
    GPSLatitudeRef = 0x00000801,
    GPSLatitude = 0x00000802,
    GPSLongitudeRef = 0x00000803,
    GPSLongitude = 0x00000804,
    GPSAltitudeRef = 0x00000805,
    GPSAltitude = 0x00000806,
    GPSTimeStamp = 0x00000807,
    GPSSatellites = 0x00000808,
    GPSStatus = 0x00000809,
    GPSMapDatum = 0x00000812,
    GPSDateStamp = 0x0000081D,

    //Property Mask
    AtCapture_Flag = unchecked((uint)0x80000000),

    //Capture Properties
    AEMode = 0x00000400,
    DriveMode = 0x00000401,
    ISO = 0x00000402,
    MeteringMode = 0x00000403,
    AFMode = 0x00000404,
    Av = 0x00000405,
    Tv = 0x00000406,
    ExposureCompensation = 0x00000407,
    FlashCompensation = 0x00000408,
    FocalLength = 0x00000409,
    AvailableShots = 0x0000040a,
    Bracket = 0x0000040b,
    WhiteBalanceBracket = 0x0000040c,
    LensName = 0x0000040d,
    AEBracket = 0x0000040e,
    FEBracket = 0x0000040f,
    ISOBracket = 0x00000410,
    NoiseReduction = 0x00000411,
    FlashOn = 0x00000412,
    RedEye = 0x00000413,
    FlashMode = 0x00000414,
    LensStatus = 0x00000416,
    Artist = 0x00000418,
    Copyright = 0x00000419,
    DepthOfField = 0x0000041b,
    EFCompensation = 0x0000041e,
    AEModeSelect = 0x00000436,

    //EVF Properties
    Evf_OutputDevice = 0x00000500,
    Evf_Mode = 0x00000501,
    Evf_WhiteBalance = 0x00000502,
    Evf_ColorTemperature = 0x00000503,
    Evf_DepthOfFieldPreview = 0x00000504,

    //EVF IMAGE DATA Properties
    Evf_Zoom = 0x00000507,
    Evf_ZoomPosition = 0x00000508,
    Evf_FocusAid = 0x00000509,
    Evf_ImagePosition = 0x0000050B,
    Evf_HistogramStatus = 0x0000050C,
    Evf_AFMode = 0x0000050E,

    Record = 0x00000510,

    Evf_HistogramY = 0x00000515,
    Evf_HistogramR = 0x00000516,
    Evf_HistogramG = 0x00000517,
    Evf_HistogramB = 0x00000518,

    Evf_CoordinateSystem = 0x00000540,
    Evf_ZoomRect = 0x00000541,
    Evf_ImageClipRect = 0x00000545,
}
