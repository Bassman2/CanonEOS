namespace CanonUtility.ViewModel;

public partial class DirectoryViewModel : ObservableObject, IExplorerItem
{
    private EosFileSystemItem directoryItem;

    public DirectoryViewModel(EosFileSystemItem directoryItem)
    {
        this.directoryItem = directoryItem;
        this.Name = directoryItem.Name;

        this.Folders = directoryItem.Directories?.Select(f => new DirectoryViewModel(f)).ToList() ?? [];
        //SetHasChilden(true);
    }

    public bool IsFolder => true;

    public bool IsInitialExpanded => true;

    public string Name { get; }

    public ImageSource? Icon => null;

    public bool HasFolders => throw new NotImplementedException();

    public IEnumerable<IExplorerItem> Folders { get; }

    public bool HasItems => this.Folders.Any();

    public IEnumerable<IExplorerItem> Items => directoryItem.Files?.Select(f => new FileViewModel(f)).ToList() ?? [];

    //protected override void Update()
    //{
    //    this.Children = directoryItem.Directories?.Select(d => new DirectoryViewModel(d)).ToList() ?? [];
    //}
}
