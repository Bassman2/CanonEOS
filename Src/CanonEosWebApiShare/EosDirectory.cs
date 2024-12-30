namespace CanonEos;

public abstract class EosDirectory : EosFileSystemItem
{
    public abstract IEnumerable<EosFileSystemItem>? FileSystemItems { get; }

    public abstract IEnumerable<EosDirectory>? Directories { get; }

    public abstract IEnumerable<EosFile>? Files { get; }

}
