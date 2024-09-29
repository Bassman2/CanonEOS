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
        this.Author = orgAuthor = camera.Author;
        this.Owner = orgOwner = camera.Owner;
        this.Nickname = orgNickname = camera.Nickname;
        this.DateTime = orgDateTime = camera.DateTime;
        this.Beep = orgBeep = camera.Beep;
        this.DisplayOff = orgDisplayOff = camera.DisplayOff;
        this.AutoPowerOff = orgAutoPowerOff = camera.AutoPowerOff;

    }

    private string? orgCopyright;
    private string? orgAuthor;
    private string? orgOwner;
    private string? orgNickname;
    private DateTime? orgDateTime;
    private string? orgBeep;
    private string? orgDisplayOff;
    private string? orgAutoPowerOff;

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
    private string? nickname;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private DateTime? dateTime;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? beep;

    public string[]? BeepValues => camera?.BeepValues; 

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? displayOff;

    public string[]? DisplayOffValues => camera?.DisplayOffValues;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateCommand))]
    private string? autoPowerOff;

    public string[]? AutoPowerOffValues => camera?.AutoPowerOffValues;

    public bool OnCanUpdate() => (Copyright, Author, Owner, Nickname, DateTime, Beep, DisplayOff, AutoPowerOff) != (orgCopyright, orgAuthor, orgOwner, orgNickname, orgDateTime, orgBeep, orgDisplayOff, orgAutoPowerOff);

    [RelayCommand(CanExecute = nameof(OnCanUpdate))]
    private void OnUpdate()
    {
        if (this.Copyright != orgCopyright)
        {
            camera!.Copyright = orgCopyright = this.Copyright;
        }
        if (this.Author != orgAuthor)
        {
            camera!.Author = orgAuthor = this.Author;
        }
        if (this.Owner != orgOwner)
        {
            camera!.Owner = orgOwner = this.Owner;
        }
        if (this.Nickname != orgNickname)
        {
            camera!.Nickname = orgNickname = this.Nickname;
        }
        if (this.DateTime != orgDateTime)
        {
            camera!.DateTime = orgDateTime = this.DateTime;
        }
        if (this.Beep != orgBeep)
        {
            camera!.Beep = orgBeep = this.Beep;
        }
        if (this.DisplayOff != orgDisplayOff)
        {
            camera!.DisplayOff = orgDisplayOff = this.DisplayOff;
        }
        if (this.AutoPowerOff != orgAutoPowerOff)
        {
            camera!.AutoPowerOff = orgAutoPowerOff = this.AutoPowerOff;
        }
    }
    
}
