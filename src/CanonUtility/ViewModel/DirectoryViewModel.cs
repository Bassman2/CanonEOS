
namespace CanonUtility.ViewModel;

public partial class DirectoryViewModel : ObservableObject, IExplorerItem
{
    private DirectoryItem directoryItem;

    public DirectoryViewModel(DirectoryItem directoryItem)
    {
        this.directoryItem = directoryItem;
        this.Name = directoryItem.Name;

        SetHasChilden(true);
    }

    public bool IsFolder => throw new NotImplementedException();

    public bool IsInitialExpanded => throw new NotImplementedException();

    public string Name => throw new NotImplementedException();

    public ImageSource? Icon => throw new NotImplementedException();

    public bool HasFolders => throw new NotImplementedException();

    public IEnumerable<IExplorerItem> Folders => throw new NotImplementedException();

    public bool HasItems => throw new NotImplementedException();

    public IEnumerable<IExplorerItem> Items => throw new NotImplementedException();

    protected override void Update()
    {
        this.Children = directoryItem.Directories?.Select(d => new DirectoryViewModel(d)).ToList() ?? [];
    }
}
