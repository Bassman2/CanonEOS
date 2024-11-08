namespace CanonEos;

public class Property(EdsPropertyID id, int param, EdsDataType dataType, object? value)
{
    public EdsPropertyID Id { get; } = id;
    public int Param { get; } = param;

    public EdsDataType DataType { get; } = dataType;
    public object? Value { get; } = value ?? null;

    public string ValueString
    {
        get
        {
            if (value == null)
            {
                return "";
            }
            if (value.GetType().IsArray)
            {
                string res = "";
                Array arr = (Array)value;
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

            return value?.ToString() ?? "";
        }
    }
}
