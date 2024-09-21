namespace CanonWpf.Converter;

[ValueConversion(typeof(object), typeof(IEnumerable<DirectoryItem>))]
public class ListConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DirectoryItem item)
        {
            return item.DirectoryItems;
        }
        if (value is Volume volume)
        {
            return volume.DirectoryItems;
        }
        return null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
