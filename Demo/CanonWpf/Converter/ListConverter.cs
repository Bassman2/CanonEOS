namespace CanonWpf.Converter;

[ValueConversion(typeof(object), typeof(IEnumerable<EosFileSystemItem>))]
public class ListConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is EosDirectory directory)
        {
            return directory.FileSystemItems;
        }
        if (value is EosVolume volume)
        {
            return volume.FileSystemItems;
        }
        return null;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
