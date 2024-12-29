namespace CanonUtility.ViewModel;

public partial class CameraInfoViewModel : ObservableObject
{
    private readonly Camera camera;
    public CameraInfoViewModel(Camera camera)
    {
        this.camera = camera;
        this.Volumes = camera.Volumes?.Select(v => new VolumeViewModel(camera, v)).ToList() ?? [];
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
    private List<VolumeViewModel> volumes = [];

    #endregion

    

    #region settings

    public string? Copyright { get => this.camera.Copyright; }

    public string? Author { get => this.camera.Author; }

    public string? Owner { get => this.camera.Owner; }

    public string? Nickname { get => this.camera.Nickname; }

    public DateTime? DateTime { get => this.camera.DateTime; }

    public string? Beep { get => this.camera.Beep; }

    public string? DisplayOff { get => this.camera.DisplayOff; }

    public string? AutoPowerOff { get => this.camera.AutoPowerOff; }

    #endregion


    [ObservableProperty]
    private EosFileSystemItem? selectedFolder;

    [ObservableProperty]
    private List<Property>? properties;

    public Camera Camera => this.camera;

    [ObservableProperty]
    private EosFileSystemItem? selectedDirectoryItem;

    partial void OnSelectedDirectoryItemChanged(EosFileSystemItem? oldValue, EosFileSystemItem? newValue)
    {
    }

}
