namespace CanonEos;
public enum AutoPowerOff : uint
{
    [EnumMember(Value = "30")]
    [Description("Time until power off: 30 seconds")]
    _30 = 30,

    [EnumMember(Value = "60")]
    [Description("Time until power off: 60 seconds")]
    _60 = 60,

    [EnumMember(Value = "120")]
    [Description("Time until power off: 120 seconds")]
    _120 = 120,

    [EnumMember(Value = "180")]
    [Description("Time until power off: 180 seconds")]
    _180 = 180,

    [EnumMember(Value = "300")]
    [Description("Time until power off: 300 seconds")]
    _300 = 300,

    [EnumMember(Value = "600")]
    [Description("Time until power off: 600 seconds")]
    _600 = 600,

    [EnumMember(Value = "disable")]
    [Description("Disabled (Power off is not performed.)")]
    Disable = 0,

    [EnumMember(Value = "immediately")]
    [Description("Exits CCAPI and immediately enters the Auto Power Off (Sleep) state.")]
    Immediately = 0xffffffff
}