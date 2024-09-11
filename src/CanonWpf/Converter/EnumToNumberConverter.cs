namespace CanonWpf.Converter;

[ValueConversion(typeof(Enum), typeof(uint))]
public class EnumToNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            return (uint)value;
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
