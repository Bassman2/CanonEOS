namespace CanonEos.CcApi;

public class CcVolume : EosVolume
{
    private readonly CcService service;

    internal CcVolume(CcService service, Storage storage)
    {
        this.service = service;
        this.Name = storage.Name ?? "";
        this.MaxCapacity = storage.Maxize;
        this.FreeSpaceInBytes = storage.SpaceSize;
        //var colList = this.service.GetDirectories(this.Name);

    }
      
    public override string Name { get; }

    public override EdsStorageType StorageType { get; }

    public override EdsAccess Access { get; }

    public override ulong MaxCapacity { get; }

    public override ulong FreeSpaceInBytes { get; }

    private List<CcDirectory>? directories;

    public override IEnumerable<EosFileSystemItem>? FileSystemItems 
        => Directories;

    public override IEnumerable<CcDirectory>? Directories 
        => directories ??= service.GetDirectories(this.Name)?.Select(d => new CcDirectory(this.service, d)).ToList();

    public override IEnumerable<EosFileSystemItem>? Files 
        => null; 

    public override void Format()
    {
        throw new NotImplementedException();
    }

    public override void Refresh()
    {
        directories = null; // reset
    }
}
