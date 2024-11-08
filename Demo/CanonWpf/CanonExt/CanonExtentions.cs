

namespace CanonWpf.CanonExt;

public static class CanonExtentions
{

    public static BitmapImage DownloadImage(this EosFileSystemItem directoryItem)
    {
        using var stream = new MemoryStream();

        BitmapImage bitmap = new();
        bitmap.BeginInit();
        bitmap.StreamSource = stream;
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.EndInit();
        bitmap.Freeze();
        return bitmap;
    }
}
