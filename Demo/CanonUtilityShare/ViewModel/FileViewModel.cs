namespace CanonUtility.ViewModel;

public partial class FileViewModel : ObservableObject, IExplorerItem
{
    EosFileSystemItem item;

    public FileViewModel(EosFileSystemItem item)
    {
        this.item = item;
    }

    public bool IsFolder => false;

    public bool IsInitialExpanded => false;

    public string Name => item.Name;

    public ImageSource? Icon => null;

    public bool HasFolders => false;

    public IEnumerable<IExplorerItem> Folders => [];

    public bool HasItems => false;

    public IEnumerable<IExplorerItem> Items => [];
}
