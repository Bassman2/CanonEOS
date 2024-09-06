namespace CanonUtility.Converter;

[ValueConversion(typeof(object), typeof(string))]
public class EnumToDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum)
        {
            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString()!);
            DescriptionAttribute? attribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return (attribute == null ? value.ToString() : attribute.Description)!;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
