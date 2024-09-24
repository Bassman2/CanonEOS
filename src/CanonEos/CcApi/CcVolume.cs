namespace CanonEos.CcApi;

internal class CcVolume : Volume
{
    private readonly CcService service;
    public CcVolume(CcService service, Storage storage)
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

    private List<DirectoryItem>? directoryItems;
    public override IEnumerable<DirectoryItem>? DirectoryItems 
        => directoryItems ??= service.GetDirectories(this.Name)?.Select(d => (DirectoryItem)new CcDirectoryItem(this.service, d)).ToList();

    public override IEnumerable<DirectoryItem>? Directories 
        => DirectoryItems?.Where(d => d.IsFolder); 

    public override IEnumerable<DirectoryItem>? Files 
        => DirectoryItems?.Where(d => !d.IsFolder); 

    public override void Format()
    {
        throw new NotImplementedException();
    }
}
