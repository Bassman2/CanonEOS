namespace CanonWpf.Converter;

[ValueConversion(typeof(ConnectionType), typeof(BitmapImage))]
public class ConnectionTypeToImageConverter : IValueConverter
{
    static BitmapImage? usbImage;
    static BitmapImage? wifiImage;

    static ConnectionTypeToImageConverter()
    {
        usbImage = new BitmapImage(new Uri("pack://application:,,,/CanonWpf;component/Images/usb16.png"));
        wifiImage = new BitmapImage(new Uri("pack://application:,,,/CanonWpf;component/Images/wifi16.png"));
        usbImage.Freeze();
        wifiImage.Freeze();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ConnectionType connectionType)
        {
            return connectionType switch
            { 
                ConnectionType.USB => usbImage!,
                ConnectionType.WiFi => wifiImage!,
                _ => throw new NotSupportedException()
            };
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
