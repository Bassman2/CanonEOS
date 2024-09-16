namespace CanonEos.EdSdk;

internal class EdDirectoryItem : DirectoryItem
{
    private static readonly DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    private readonly nint item;

    internal EdDirectoryItem(nint item)
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

    public override ulong Size { get; }

    public override bool IsFolder { get; }

    public override uint GroupID { get; }

    public override uint Option { get; }

    public override string Name { get; }

    public override EdsImageType Format { get; }

    public override DateTime DateTime { get; }



    //public int Width { get; }
    //public int Height { get; }
    //public int NumOfComponents { get; }
    //public int ComponentDepth { get; }
    //public EdsRectangle EffectiveRect { get; }
    //public uint Reserved1 { get; }
    //public uint Reserved2 { get; }







    public override EdsFileAttribute Attribute
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

    public override IEnumerable<DirectoryItem> DirectoryItems
    {
        get => Eds.GetChildren(item).Select(i => new EdDirectoryItem(i));
    }

    public override IEnumerable<DirectoryItem> Directories
    {
        get => DirectoryItems.Where(d => d.IsFolder);
    }

    public override IEnumerable<DirectoryItem> Files
    {
        get => DirectoryItems.Where(d => !d.IsFolder);
    }

    public override void DownloadThumbnail(string filePath)
    {
        Eds.EdsCreateFileStream(filePath, EdsFileCreateDisposition.CreateNew, EdsFileAccess.Write, out nint fileStream);

        Eds.EdsDownloadThumbnail(this.item, fileStream);
    }

    public override void Download(string filePath)
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

    public override void Delete()
    {
        Eds.EdsDeleteDirectoryItem(this.item);
    }

    public override EdsImage DownloadImage()
    {
        Eds.CheckError(Eds.EdsCreateFileStream(this.Name, EdsFileCreateDisposition.CreateAlways, EdsFileAccess.ReadWrite, out nint stream));

        Eds.CheckError(Eds.EdsDownload(this.item, this.Size, stream));

        Eds.CheckError(Eds.EdsDownloadComplete(this.item));

        Eds.CheckError(Eds.EdsCreateImageRef(stream, out nint image));
        return new EdsImage(image);
    }

    public override EdsImage? DownloadStream()
    {

        Eds.CheckError(Eds.EdsCreateMemoryStream((long)this.Size, out nint stream));

        Eds.CheckError(Eds.EdsDownload(this.item, this.Size, stream));

        Eds.CheckError(Eds.EdsDownloadComplete(this.item));

        return null;
    }
}
