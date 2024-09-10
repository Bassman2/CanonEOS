namespace CanonUtility.Converter;

[ValueConversion(typeof(Enum), typeof(string))]
public class EnumToHexConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            return "0x" + ((uint)value).ToString("X8");
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
