namespace CanonAPI;

public class Volume
{
    private readonly nint volume;

    internal Volume(nint volume)
    {
        this.volume = volume;

        Eds.CheckError(Eds.EdsGetVolumeInfo(volume, out EdsVolumeInfo info));

        this.Name = info.VolumeLabel;
    }

    public string Name { get; }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => Eds.GetChildren(volume).Select(i => new DirectoryItem(i));
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
