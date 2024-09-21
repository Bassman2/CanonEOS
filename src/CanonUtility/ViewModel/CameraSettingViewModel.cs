namespace CanonUtility.ViewModel;

public partial class CameraSettingViewModel : ObservableObject
{
    private readonly Camera? camera;

    //public Camera Camera { get; init; }

    //[Obsolete("For VS only")]
    public CameraSettingViewModel()
    { }

    public CameraSettingViewModel(Camera camera)
    {
        this.camera = camera;
        this.Copyright = orgCopyright = camera.Copyright;
        this.Author = orgAuthor = camera.Artist;
        this.Owner = orgOwner = camera.OwnerName;
        this.DateTime = orgDateTime = camera.DateTime;
    }

    private string? orgCopyright;
    private string? orgAuthor;
    private string? orgOwner;
    private DateTime? orgDateTime;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? copyright;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? author;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? owner;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private DateTime? dateTime;

    public bool OnCanUpdate() => (Copyright, Author, Owner) != (orgCopyright, orgAuthor, orgOwner);

    [RelayCommand(CanExecute = nameof(OnCanUpdate))]
    private void OnUpdate()
    {
        if (this.Copyright != orgCopyright)
        {
            camera!.Copyright = this.Copyright;
        }
        if (this.Author != orgAuthor)
        {
            camera!.Artist = this.Author;
        }
        if (this.Owner != orgOwner)
        {
            camera!.OwnerName = this.Owner;
        }
    }
    
}
