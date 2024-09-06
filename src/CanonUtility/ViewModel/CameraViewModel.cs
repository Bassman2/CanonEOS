namespace CanonUtility.ViewModel;

public partial class CameraViewModel : ObservableObject, IDisposable
{
    private readonly Camera camera;
    public CameraViewModel(Camera camera)
    {
        this.camera = camera;
        this.Volumes = camera.Volumes.ToList() ?? [];
        this.Properties = camera.Properties.ToList();
    }

    public void Dispose()
    {
        this.camera.Dispose();
    }

    public string Name { get => this.camera.Name; }
    public string? ProductName { get => this.camera.ProductName; }
    public string? OwnerName { get => this.camera.OwnerName; }
    public string? FirmwareVersion { get => this.camera.FirmwareVersion; }
    public string? CurrentStorage { get => this.camera.CurrentStorage; }
    public string? CurrentFolder { get => this.camera.CurrentFolder; }
    public string? BodyIDEx { get => this.camera.BodyIDEx; }
    public string? LensName { get => this.camera.LensName; }
    public string? Artist { get => this.camera.Artist; }
    public string? Copyright { get => this.camera.Copyright; }

    [ObservableProperty]
    private List<Volume> volumes = [];

    [ObservableProperty]
    private DirectoryItem? selectedFolder;

    [ObservableProperty]
    private List<Property>? properties;


}
