namespace CanonUtility.ViewModel;

public partial class PictureViewModel : ObservableObject
{
    private readonly Camera camera;
    
    // for Designer
    public PictureViewModel()
    { 
        // dummy camera
        this.camera = new CcCamera();
    }

    public PictureViewModel(Camera camera)
    {
        this.camera = camera;
        this.Items = camera.Volumes?.Select(v => new VolumeViewModel(camera, v)).ToArray();
    }

    //public Camera Camera { get; }

    [ObservableProperty]
    private IEnumerable<VolumeViewModel>? items;

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
