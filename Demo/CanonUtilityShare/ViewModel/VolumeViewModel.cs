namespace CanonUtility.ViewModel;

public partial class VolumeViewModel : ObservableObject, IExplorerItem
{
    private EosVolume volume;
    public VolumeViewModel(Camera camera, EosVolume volume)
    {
        this.volume = volume;
        this.Name = volume.Name;

        this.Folders = volume.Directories?.Select(f => new DirectoryViewModel(f)).ToList() ?? [];
        //SetHasChilden(true);
    }

    public bool IsFolder => true;

    public bool IsInitialExpanded => true;

    public string Name { get; }

    public ImageSource? Icon => null;

    public bool HasFolders => this.Folders.Any();

    public IEnumerable<IExplorerItem> Folders { get; }

    public bool HasItems => false;

    public IEnumerable<IExplorerItem> Items => [];
}
