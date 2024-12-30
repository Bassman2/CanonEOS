namespace CanonEos;

public abstract class EosVolume
{
    public abstract string Name { get; }

    public abstract EdsStorageType StorageType { get; }

    public abstract EdsAccess Access { get; }

    public abstract ulong MaxCapacity { get; }

    public abstract ulong FreeSpaceInBytes { get; }


    public abstract IEnumerable<EosFileSystemItem>? FileSystemItems { get; }

    public abstract IEnumerable<EosFileSystemItem>? Directories { get; }

    public abstract IEnumerable<EosFileSystemItem>? Files {  get; }

    public abstract void Format();
    public abstract void Refresh();
}
