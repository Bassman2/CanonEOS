namespace CanonWpf.Converter;

[ValueConversion(typeof(Camera), typeof(IEnumerable<EosVolume>))]
public class SourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<Camera> cameras)
        {
            return cameras;
        }
        if (value is Camera camera)
        {
            return new[] { camera };
        }
        if (value is IEnumerable<EosVolume> volumes)
        {
            return volumes;
        }
        if (value is EosVolume volume)
        {
            return new[] { volume };
        }
#pragma warning disable CS8603 // Possible null reference return.
        return value;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}