namespace CanonUtility.ViewModel;

public partial class PicturePropertyViewModel : ObservableObject
{

    public PicturePropertyViewModel(Camera camera)
    {
        this.Camera = camera;
    }

    public Camera Camera { get; }

    [ObservableProperty]
    private DirectoryItem? selectedDirectoryItem;

    partial void OnSelectedDirectoryItemChanged(DirectoryItem? oldValue, DirectoryItem? newValue)
    {
        if (newValue != null && !newValue.IsFolder && newValue.Format != EdsImageType.Unknown)
        {
            EdsImage image = newValue.DownloadImage();

            this.Properties = image.Properties.ToList();

        }
        else
        {
            this.Properties = null;
        }
    }

    [ObservableProperty]
    private IEnumerable<Property>? properties;
}
