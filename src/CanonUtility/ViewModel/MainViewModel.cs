namespace CanonUtility.ViewModel;

public partial class MainViewModel : AppViewModel, IDisposable
{
    private readonly Canon library;
    public MainViewModel() 
    {
        this.library = new();
        this.library.CameraAdded += OnCameraAdded;
        //this.Canon = library.IsInitialized ? "Connected" : "Disconnected";
        this.Cameras = this.library.GetCameras().ToList();
        this.SelectedCamera = this.Cameras.FirstOrDefault();
    }

    private void OnCameraAdded(Canon sender)
    {
        this.Cameras = this.library.GetCameras().ToList();
        this.SelectedCamera = this.Cameras.FirstOrDefault();
    }

    public void Dispose()
    {
        this.SelectedCamera?.Dispose();
        this.library.Dispose();
    }

    [ObservableProperty]
    private string state = "Ready";

    //[ObservableProperty]
    //private string canon = "Disconnected";

    [ObservableProperty]
    private string camera = "Empty";

    [ObservableProperty]
    private Version fileVersion = Canon.EdsdkFileVersion;

    [ObservableProperty]
    private Version productVersion = Canon.EdsdkProductVersion;

    [ObservableProperty]
    private string path = Canon.EdsdkPath;

    [ObservableProperty]
    private string process = Environment.Is64BitProcess ? "x64" : "x86";
   
    [ObservableProperty]
    private List<Camera> cameras = [];

    [ObservableProperty]
    private Camera? selectedCamera;

    partial void OnSelectedCameraChanged(Camera? oldValue, Camera? newValue)
    {
        CameraViewModel?.Dispose();
        CameraViewModel = new CameraViewModel(newValue!);  
        
        this.PictureViewModel = newValue != null ? new PictureViewModel(newValue) : null;
        this.PicturePropertyViewModel = newValue != null ? new PicturePropertyViewModel(newValue) : null;
    }

    [ObservableProperty]
    private CameraViewModel? cameraViewModel;

    [ObservableProperty]
    private PictureViewModel? pictureViewModel;

    [ObservableProperty]
    private PicturePropertyViewModel? picturePropertyViewModel;

    protected override void OnActivate()
    {
        base.OnActivate();        
    }

    protected override bool OnClosing()
    {
        this.library.Dispose();
        return base.OnClosing();
    }    
}
