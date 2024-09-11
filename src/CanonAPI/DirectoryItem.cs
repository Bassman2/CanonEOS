namespace CanonAPI;

public class DirectoryItem
{
    private static readonly DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    private readonly nint item;

    internal DirectoryItem(nint item)
    {
        this.item = item;

        Eds.CheckError(Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info));

        this.Size = info.Size64;
        this.IsFolder = info.IsFolder;
        this.GroupID = info.GroupID;
        this.Option = info.Option;
        this.Name = info.FileName;
        this.Format = (EdsImageType)(((int)info.Format) & 0xf);
        this.DateTime = unixDateTime.AddSeconds(info.DateTime).ToLocalTime();

        //if (this.IsFolder == false)
        //{
        //    Eds.CheckError(Eds.EdsGetImageInfo(item, EdsImageSource.FullView, out EdsImageInfo imageInfo));

        //    this.Width = imageInfo.Width;
        //    this.Height = imageInfo.Height;
        //    this.NumOfComponents = imageInfo.NumOfComponents;
        //    this.ComponentDepth = imageInfo.ComponentDepth;
        //    this.EffectiveRect = imageInfo.EffectiveRect;
        //    this.Reserved1 = imageInfo.Reserved1;
        //    this.Reserved2 = imageInfo.Reserved2;
        //}
    }

    public ulong Size { get; }

    public bool IsFolder { get; }

    public uint GroupID { get; }

    public uint Option { get; }

    public string Name { get; }
    
    public EdsImageType Format { get; }

    public DateTime DateTime { get; }

    //public int Width { get; }
    //public int Height { get; }
    //public int NumOfComponents { get; }
    //public int ComponentDepth { get; }
    //public EdsRectangle EffectiveRect { get; }
    //public uint Reserved1 { get; }
    //public uint Reserved2 { get; }







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

    
    public IEnumerable<Property> Properties
    {
        get => Eds.GetPictureProperties(item);
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

    public EdsImage DownloadImage()
    {
        

        Eds.CheckError(Eds.EdsCreateFileStream(this.Name, EdsFileCreateDisposition.CreateAlways, EdsFileAccess.ReadWrite, out nint stream));

        Eds.CheckError(Eds.EdsDownload(this.item, this.Size, stream));

        Eds.CheckError(Eds.EdsDownloadComplete(this.item));

        Eds.CheckError(Eds.EdsCreateImageRef(stream, out nint image));
        return new EdsImage(image);
    }
}
