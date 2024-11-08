namespace CanonEos;

public enum Beep
{
    [EnumMember(Value = "enable")]
    [Description("Beep sound enabled")]
    Enable, 

    [EnumMember(Value = "disable")]
    [Description("Beep sound disabled")]
    Disabled,

    [EnumMember(Value = "disabletouch")]
    [Description("Beep sound disabled only for touch sound")]
    Disabletouch,
}
