
namespace CanonEos.CcApi;

public class CcFile : EosFile
{
    private readonly CcService service;

    private readonly string? volume;
    private readonly string? folder;

    internal CcFile(CcService service, string path)
    {
        this.service = service;
        string[] arr = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length != 6) throw new CcException("Wrong path length");

        this.volume = arr[3];
        this.folder = arr[4];
        this.Name = arr[5];
        this.FullName = $"{arr[3]}/{arr[4]}/{arr[5]}";
    }

    public override string Name { get; }

    public override string FullName { get; }

    public override ulong Size { get; }

    public override DateTime CreationTime { get; }

    public override bool IsFolder => false;

    public override void Delete()
    {
        throw new NotImplementedException();
    }

    public override void Download(string filePath)
    {
        throw new NotImplementedException();
    }

    public override void DownloadThumbnail(string filePath)
    {
        throw new NotImplementedException();
    }

    public override void Refresh()
    {
        throw new NotImplementedException();
    }
}
