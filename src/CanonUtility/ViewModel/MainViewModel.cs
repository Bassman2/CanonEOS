namespace CanonUtility.ViewModel;

public partial class MainViewModel : AppViewModel
{
    private readonly Canon canon;
    public MainViewModel() 
    {
        this.canon = new();
        this.Cameras = this.canon.GetCameras().ToList();
        this.SelectedCamera = this.Cameras.FirstOrDefault();
    }

    [ObservableProperty]
    private List<Camera> cameras = [];

    [ObservableProperty]
    private Camera? selectedCamera;

    protected override void OnActivate()
    {
        base.OnActivate();
        
    }

    protected override bool OnClosing()
    {
        this.canon.Dispose();
        return base.OnClosing();
    }
    
}
