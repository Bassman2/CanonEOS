namespace CanonEos.CcApi;

public class CcDirectoryItem : DirectoryItem
{
    private readonly CcService service;
    
    private readonly string? volume;
    private readonly string? folder;
    internal CcDirectoryItem(CcService service, string path)
    {
        this.service = service;
        string[] arr = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (arr.Length == 4)
        {
            volume = arr[3];
            Name = arr[3];
            IsFolder = true;
        }
        else if (arr.Length == 5)
        {
            volume = arr[3];
            folder = arr[4];
            Name = arr[4];
            IsFolder = true;
        }
        else if (arr.Length == 6)
        {
            volume = arr[3];
            folder = arr[4];
            Name = arr[5];
            IsFolder = false;
        }
        else
        {
            throw new Exception();
        }
    }

    public override string Name { get; }

    public override ulong Size { get; }

    public override bool IsFolder { get; }

    public override uint GroupID { get; }

    public override uint Option { get; }


    public override EdsImageType Format { get; }

    public override DateTime DateTime { get; }

    public override EdsFileAttribute Attribute { get; set; }

    private List<DirectoryItem>? directoryItems;
    public override IEnumerable<DirectoryItem>? DirectoryItems 
        => directoryItems ??= service.GetFiles(volume!, folder!)?.Select(d => (DirectoryItem)new CcDirectoryItem(this.service, d)).ToList(); 
    

    public override IEnumerable<DirectoryItem>? Directories 
        => DirectoryItems?.Where(d => d.IsFolder); 
    

    public override IEnumerable<DirectoryItem>? Files 
        => DirectoryItems?.Where(d => !d.IsFolder); 

    public override void DownloadThumbnail(string filePath)
    {
        throw new NotImplementedException();
    }

    public override void Download(string filePath)
    {
        throw new NotImplementedException();
    }

    public override void Delete()
    {
        throw new NotImplementedException();
    }

    public override EdsImage DownloadImage()
    {
        throw new NotImplementedException();
    }

    public override EdsImage? DownloadStream()
    {
        throw new NotImplementedException();
    }
}
