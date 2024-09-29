namespace CanonUtility.ViewModel;

public partial class DirectoryViewModel : TreeItemViewModel
{
    private DirectoryItem directoryItem;

    public DirectoryViewModel(DirectoryItem directoryItem)
    {
        this.directoryItem = directoryItem;
        this.Name = directoryItem.Name;

        SetHasChilden(true);
    }

    protected override void Update()
    {
        this.Children = directoryItem.Directories?.Select(d => new DirectoryViewModel(d)).ToList() ?? [];
    }
}
