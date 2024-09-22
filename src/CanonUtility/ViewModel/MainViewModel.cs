using CanonEos.CcApi;

namespace CanonUtility.ViewModel;

public partial class MainViewModel : AppViewModel, IDisposable
{
    private readonly Canon library;

    private readonly Camera? ccCamera;

    public MainViewModel() 
    {
        var cameras = CcFinder.FindCameras()?.ToList();

        this.library = new();
        this.library.CameraAdded += OnCameraAdded;
        //this.Canon = library.IsInitialized ? "Connected" : "Disconnected";
        this.Cameras = this.library.GetCameras().ToList();

        this.ccCamera = Canon.AddCcCamera("192.168.178.67");
        
        if (this.ccCamera != null)
        {
            this.Cameras.Add(this.ccCamera);
        }
        this.SelectedCamera = this.Cameras.FirstOrDefault();

    }

    private void OnCameraAdded(Canon sender)
    {
        this.Cameras = this.library.GetCameras().ToList();
        if (this.ccCamera != null)
        {
            this.Cameras.Add(this.ccCamera);
        }
        this.SelectedCamera = this.Cameras.FirstOrDefault();
    }

    public void Dispose()
    {
        this.SelectedCamera?.Dispose();
        this.library.Dispose();
        GC.SuppressFinalize(this);  
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
        if (newValue is not null)
        {
            this.CameraViewModel = new CameraViewModel(newValue!);
            this.CameraSettingViewModel = new CameraSettingViewModel(newValue!);
            this.PictureViewModel = new PictureViewModel(newValue);
            this.PicturePropertyViewModel = new PicturePropertyViewModel(newValue);
        }
        else
        {
            this.CameraViewModel = null;
            this.CameraSettingViewModel = null;
            this.PictureViewModel = null;
            this.PicturePropertyViewModel = null;
        }

    }

    [ObservableProperty]
    private CameraViewModel? cameraViewModel;

    [ObservableProperty]
    private CameraSettingViewModel? cameraSettingViewModel;

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
