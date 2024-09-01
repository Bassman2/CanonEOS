namespace CanonAPI;

public class DirectoryItem
{
    private readonly nint item;

    internal DirectoryItem(nint item)
    {
        this.item = item;

        Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);

        this.Name = info.FileName;
        this.IsFolder = info.IsFolder;
    }

    public string Name { get; }

    public bool IsFolder { get; }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => Eds.GetChildren(item).Select(i => new DirectoryItem(i));
    }

    public IEnumerable<DirectoryItem> Directories
    {
        get => DirectoryItems.Where(d => d.IsFolder);
    }

    public IEnumerable<DirectoryItem> Files
    {
        get => DirectoryItems.Where(d => !d.IsFolder);
    }
}
