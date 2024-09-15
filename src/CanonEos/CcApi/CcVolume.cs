namespace CanonEos.CcApi;

internal class CcVolume : Volume
{
    public CcVolume()
    {
        this.Name = "";
    }
    public override string Name { get; }

    public override EdsStorageType StorageType { get; }

    public override EdsAccess Access { get; }

    public override ulong MaxCapacity { get; }

    public override ulong FreeSpaceInBytes { get; }


    public override IEnumerable<DirectoryItem> DirectoryItems { get => []; }

    public override IEnumerable<DirectoryItem> Directories { get => []; }

    public override IEnumerable<DirectoryItem> Files { get => []; }

    public override void Format()
    {
        throw new NotImplementedException();
    }
}
