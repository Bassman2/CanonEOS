namespace CanonAPI;

public class Volume
{
    private readonly nint volume;

    internal Volume(nint volume)
    {
        this.volume = volume;

        CanonSDK.EdsGetVolumeInfo(volume, out EdsVolumeInfo info);

        this.Name = info.szVolumeLabel;
    }

    public string Name { get; }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => CanonSDK.GetChildren(volume).Select(i => new DirectoryItem(i));
    }
}
