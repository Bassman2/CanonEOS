namespace CanonEos;

#if NET8_0_OR_GREATER
public class Property(EdsPropertyID id, int param, EdsDataType dataType, object? value)
{
    public EdsPropertyID Id { get; } = id;
    public int Param { get; } = param;
    public EdsDataType DataType { get; } = dataType;
    public object? Value { get; } = value ?? null;
#else
public class Property
{
    public EdsPropertyID Id { get; }
    public int Param { get; }

    public EdsDataType DataType { get; }
    public object? Value { get; }  

    public Property(EdsPropertyID id, int param, EdsDataType dataType, object? value)
    {
        this.Id = id;
        this.Param = param;
        this.DataType = dataType;
        this.Value = value;
    }
#endif

    public string ValueString
    {
        get
        {
            if (Value == null)
            {
                return "";
            }
            if (Value.GetType().IsArray)
            {
                string res = "";
                Array arr = (Array)Value;
                foreach (object item in arr)
                {
                    res += item.ToString() + ",";
                }
                return res.Trim(',');
            }
            //if (value.GetType() == typeof(EdsFocusInfo))
            //{ }
            //if (value.GetType() == typeof(EdsPictureStyleDesc))
            //{ }

            return Value?.ToString() ?? "";
        }
    }
}
