

namespace CanonUtility.ViewModel;

public partial class FileViewModel : ObservableObject, IExplorerItem
{
    public bool IsFolder => throw new NotImplementedException();

    public bool IsInitialExpanded => throw new NotImplementedException();

    public string Name => throw new NotImplementedException();

    public ImageSource? Icon => throw new NotImplementedException();

    public bool HasFolders => throw new NotImplementedException();

    public IEnumerable<IExplorerItem> Folders => throw new NotImplementedException();

    public bool HasItems => throw new NotImplementedException();

    public IEnumerable<IExplorerItem> Items => throw new NotImplementedException();
}
