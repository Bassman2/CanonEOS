namespace CanonEos;

public abstract class Volume
{
    public abstract string Name { get; }

    public abstract EdsStorageType StorageType { get; }

    public abstract EdsAccess Access { get; }

    public abstract ulong MaxCapacity { get; }

    public abstract ulong FreeSpaceInBytes { get; }


    public abstract IEnumerable<DirectoryItem>? DirectoryItems { get; }

    public abstract IEnumerable<DirectoryItem>? Directories { get; }

    public abstract IEnumerable<DirectoryItem>? Files {  get; }

    public abstract void Format();
}
