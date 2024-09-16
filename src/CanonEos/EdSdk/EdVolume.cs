namespace CanonEos.EdSdk;

internal class EdVolume : Volume
{
    private readonly nint volume;

    internal EdVolume(nint volume)
    {
        this.volume = volume;

        Eds.CheckError(Eds.EdsGetVolumeInfo(volume, out EdsVolumeInfo info));

        this.StorageType = info.StorageType;
        this.Access = info.Access;
        this.MaxCapacity = info.MaxCapacity;
        this.FreeSpaceInBytes = info.FreeSpaceInBytes;
        this.Name = info.VolumeLabel;
    }

    public override EdsStorageType StorageType { get; }

    public override EdsAccess Access { get; }

    public override ulong MaxCapacity { get; }

    public override ulong FreeSpaceInBytes { get; }

    public override string Name { get; }


    public override IEnumerable<DirectoryItem> DirectoryItems
    {
        get => Eds.GetChildren(volume).Select(i => new EdDirectoryItem(i));
    }

    public override IEnumerable<DirectoryItem> Directories
    {
        get => DirectoryItems.Where(d => d.IsFolder);
    }

    public override IEnumerable<DirectoryItem> Files
    {
        get => DirectoryItems.Where(d => !d.IsFolder);
    }

    public override void Format()
    {
        Eds.EdsFormatVolume(this.volume);
    }
}
