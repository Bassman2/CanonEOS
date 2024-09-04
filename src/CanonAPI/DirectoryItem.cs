namespace CanonAPI;

public class DirectoryItem
{
    private readonly nint item;

    internal DirectoryItem(nint item)
    {
        this.item = item;

        Eds.CheckError(Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info));

        this.Name = info.FileName;
        this.Size = info.Size64;
        this.IsFolder = info.IsFolder;
        this.Format = info.Format;
    }

    public string Name { get; }

    public long Size { get; }

    public bool IsFolder { get; }

    public EdsImageType Format { get; }

    public EdsFileAttribute Attribute 
    {
        get
        {
            Eds.EdsGetAttribute(this.item, out EdsFileAttribute attr);
            return attr;
        } 
        set 
        {
            Eds.EdsSetAttribute(this.item, value);
        }
    }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => Eds.GetChildren(item).Select(i => new DirectoryItem(i));
    }

    public IEnumerable<DirectoryItem> Directories
    {
        get => DirectoryItems.Where(d => d.IsFolder);
    }

    public IEnumerable<DirectoryItem> Files
    {
        get => DirectoryItems.Where(d => !d.IsFolder);
    }

    public void DownloadThumbnail(string filePath)
    {
        Eds.EdsCreateFileStream(filePath, EdsFileCreateDisposition.CreateNew, EdsFileAccess.Write, out nint fileStream);

        Eds.EdsDownloadThumbnail(this.item, fileStream);
    }

    public void Download(string filePath)
    {
        Eds.EdsCreateFileStream(filePath, EdsFileCreateDisposition.CreateNew, EdsFileAccess.Write, out nint fileStream);

        //Eds.EdsSetProgressCallback(item, x, EdsProgressOption.Periodically, fileStream);

        long size = 0;

        Eds.EdsDownload(this.item, size, fileStream);
        Eds.EdsCreateMemoryStream((long)(EdsConst.EDS_TRANSFER_BLOCK_SIZE * 1024), out nint stream);
        
        Eds.EdsDownload(this.item, size, stream);
        Eds.EdsDownloadComplete(this.item);
        Eds.EdsRelease(stream);
    }

    //public EdsStream Download(string File)
    //{
    //    //Eds.EdsCreateFileStream("xxx", EdsFileCreateDisposition.CreateNew, EdsFileAccess.ReadWrite, out nint outStream);
        
    //    Eds.EdsCreateMemoryStream((long)(EdsConst.EDS_TRANSFER_BLOCK_SIZE * 1024), out nint stream);
    //    long size = 0;
    //    Eds.EdsDownload(this.item, size, stream);

    //    return new EdsStream(stream);
    //}

    public void Delete()
    {
        Eds.EdsDeleteDirectoryItem(this.item);
    }
}
