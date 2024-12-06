namespace CanonEos.CcApi;

public class CcDirectory : EosDirectory
{
    private readonly CcService service;

    private readonly string? volume;
    private readonly string? folder;
    internal CcDirectory(CcService service, string path)
    {
        this.service = service;
        string[] arr = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (arr.Length == 4)
        {
            volume = arr[3];
            Name = arr[3];
            //IsFolder = true;
        }
        else if (arr.Length == 5)
        {
            volume = arr[3];
            folder = arr[4];
            Name = arr[4];
            //IsFolder = true;
        }
        else if (arr.Length == 6)
        {
            volume = arr[3];
            folder = arr[4];
            Name = arr[5];
            //IsFolder = false;
        }
        else
        {
            throw new Exception();
        }
        this.FullName = $"{arr[3]}/{arr[4]}";

    }

    public override string Name { get; }

    public override string FullName { get; }

    public override DateTime CreationTime { get; }

    public override bool IsFolder => true; 

    private List<CcFile>? files;
    public override IEnumerable<EosFileSystemItem>? FileSystemItems
        => Files;


    public override IEnumerable<CcDirectory>? Directories
        => null;


    public override IEnumerable<CcFile>? Files
    
        => files ??= service.GetFilesAsync(volume!, folder!, default).ToList().Select(d => new CcFile(this.service, d)).ToList();


    public override void Delete()
    {
        throw new NotImplementedException();
    }

    public override void Refresh()
    {
        files = null; // reset
    }
}
