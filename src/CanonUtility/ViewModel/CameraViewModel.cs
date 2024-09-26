namespace CanonUtility.ViewModel;

public partial class CameraViewModel : ObservableObject
{
    private readonly Camera camera;
    public CameraViewModel(Camera camera)
    {
        this.camera = camera;
        this.Volumes = camera.Volumes?.ToList() ?? [];
        this.Properties = camera.Properties.ToList();
    }

    #region information

    public string? Name { get => this.camera.Name; }
    public string? ProductName { get => this.camera.ProductName; }
    public string? FirmwareVersion { get => this.camera.FirmwareVersion; }
    public string? BodyIDEx { get => this.camera.BodyIDEx; }
    public string? LensName { get => this.camera.LensName; }
    public string? CurrentStorage { get => this.camera.CurrentStorage; }
    public string? CurrentFolder { get => this.camera.CurrentFolder; }

    public TemperatureStatus? TemperatureStatus { get => this.camera.TemperatureStatus; }

    public List<BatteryInfo>? Batteries => this.Camera.Batteries?.Reverse().ToList();

    [ObservableProperty]
    private List<Volume> volumes = [];

    #endregion

    

    #region settings

    public string? Copyright { get => this.camera.Copyright; }

    public string? Author { get => this.camera.Author; }

    public string? OwnerName { get => this.camera.OwnerName; }

    public DateTime? DateTime { get => this.camera.DateTime; }

    #endregion


    [ObservableProperty]
    private DirectoryItem? selectedFolder;

    [ObservableProperty]
    private List<Property>? properties;

    public Camera Camera => this.camera;

    [ObservableProperty]
    private DirectoryItem? selectedDirectoryItem;

    partial void OnSelectedDirectoryItemChanged(DirectoryItem? oldValue, DirectoryItem? newValue)
    {
    }

}
