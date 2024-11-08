namespace CanonEos;

public abstract class EosFileSystemItem
{
    public abstract string Name { get; }

    public abstract string FullName { get; }

    public abstract DateTime CreationTime { get; }

    public abstract bool IsFolder { get; }

    public abstract void Delete();

    public abstract void Refresh();  
}
