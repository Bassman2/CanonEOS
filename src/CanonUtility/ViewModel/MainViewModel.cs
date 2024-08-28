namespace CanonUtility.ViewModel;

public partial class MainViewModel : AppViewModel, IDisposable
{
    private readonly Canon library;
    public MainViewModel() 
    {
        this.library = new();
        this.Cameras = this.library.GetCameras().ToList();
        this.SelectedCamera = this.Cameras.FirstOrDefault();
    }

    public void Dispose()
    {
        this.SelectedCamera?.Dispose();
        this.library.Dispose();
    }

    [ObservableProperty]
    private string status = "Ready";

    [ObservableProperty]
    private string canon = "Disconnected";

    [ObservableProperty]
    private string camera = "Empty";

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
    }

    [ObservableProperty]
    private CameraViewModel? cameraViewModel;    

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
