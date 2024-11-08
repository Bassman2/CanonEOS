namespace CanonEos;

public enum TemperatureStatus
{
    [EnumMember(Value = "normal")]
    [Description("Normal status")]
    Normal = 0x0000,

    [EnumMember(Value = "warning")]
    [Description("Warning indication status")]
    Warning = 0x0001,

    [EnumMember(Value = "frameratedown")]
    [Description("Reduced frame rate")]
    FrameRateDown = 0x0002,

    [EnumMember(Value = "disableliveview")]
    [Description("Live View prohibited status")]
    DisableLiveView = 0x0003,

    [EnumMember(Value = "disablerelease")]
    [Description("Shooting prohibited status")]
    ShootingProhibitedStatus = 0x0004,

    [EnumMember(Value = "stillqualitywarning")]
    [Description("Degraded still image quality warning")]
    StillQualityWarning = 0x0005,



    [EnumMember(Value = "restrictionmovierecording")]
    [Description("Restriction movie recording status")]
    RestrictionMovieRecording = 0x20000,

    [EnumMember(Value = "warning_and_restrictionmovierecording")]
    [Description("Warning indication and restriction movie recording status")]
    WarningAndRestrictionMovieRecording = 0x20001,

    [EnumMember(Value = "frameratedown_and_restrictionmovierecording")]
    [Description("Reduced frame rate and restriction movie recording status")]
    FrameRateDownAndRestrictionMovieRecording = 0x20002,

    [EnumMember(Value = "disableliveview_and_restrictionmovierecording")]
    [Description("Live View prohibited and restriction movie recording status")]
    DisableLiveViewAndRestrictionMovieRecording = 0x20003,

    [EnumMember(Value = "disablerelease_and_restrictionmovierecording")]
    [Description("Shooting prohibited and restriction movie recording status")]
    DisableReleaseAndRestrictionMovieRecording = 0x20004,

    [EnumMember(Value = "stillqualitywarning_and_restrictionmovierecording")]
    [Description("Degraded still image quality warning and restriction movie recording status")]
    StillQualityWarningAndRestrictionMovieRecording = 0x20005
}
