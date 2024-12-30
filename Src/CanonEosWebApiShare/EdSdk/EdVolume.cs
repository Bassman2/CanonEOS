namespace CanonEos.EdSdk;

internal class EdVolume : EosVolume
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


    public override IEnumerable<EosFileSystemItem> FileSystemItems
    {
        get //=> Eds.GetChildren(volume).Select(i => new EdDirectory(i));
        {
            foreach (nint item in Eds.GetChildren(volume))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                yield return info.IsFolder ? new EdDirectory(item, info) : new EdFile(item, info);
            }
        }
    }

    public override IEnumerable<EosFileSystemItem> Directories
    {
        get //=> FileSystemItems.Where(d => d.IsFolder);
        {
            foreach (nint item in Eds.GetChildren(volume))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                if (info.IsFolder)
                {
                    yield return new EdDirectory(item, info);
                }
            }
        }
    }

    public override IEnumerable<EosFileSystemItem> Files
    {
        get //=> FileSystemItems.Where(d => !d.IsFolder);
        {
            foreach (nint item in Eds.GetChildren(volume))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                if (!info.IsFolder)
                {
                    yield return new EdFile(item, info);
                }
            }
        }
    }

    public override void Format()
    {
        Eds.EdsFormatVolume(this.volume);
    }

    public override void Refresh()
    {
    }
}
