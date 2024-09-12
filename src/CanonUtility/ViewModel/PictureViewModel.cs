namespace CanonUtility.ViewModel;

public partial class PictureViewModel : ObservableObject
{

    public PictureViewModel(Camera camera)
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

            var properties = image.Properties.ToList();

        }
    }

}
