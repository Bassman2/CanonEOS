namespace CanonEos.EdSdk;

public class EdFile : EosFile
{
    private static readonly DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    private readonly nint item;

    internal EdFile(nint item, EdsDirectoryItemInfo info)
    {
        this.item = item;
        this.Name = info.FileName;
        this.FullName = "ToDo";
        this.Size = info.Size64;
        this.CreationTime = unixDateTime.AddSeconds(info.DateTime).ToLocalTime();

    }

    public override ulong Size { get; }

    public override string Name { get; }

    public override string FullName { get; }

    public override DateTime CreationTime { get; }

    public override bool IsFolder => false;

    public override void Delete()
    {
        Eds.EdsDeleteDirectoryItem(this.item);
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

    

    public override void Refresh()
    {
        //Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);

        //this.Name = info.FileName;
        //this.FullName = "ToDo";
        //this.Size = info.Size64;
        //this.CreationTime = unixDateTime.AddSeconds(info.DateTime).ToLocalTime();

    }

    public EdsImage DownloadImage()
    {
        Eds.CheckError(Eds.EdsCreateFileStream(this.Name, EdsFileCreateDisposition.CreateAlways, EdsFileAccess.ReadWrite, out nint stream));

        Eds.CheckError(Eds.EdsDownload(this.item, this.Size, stream));

        Eds.CheckError(Eds.EdsDownloadComplete(this.item));

        Eds.CheckError(Eds.EdsCreateImageRef(stream, out nint image));
        return new EdsImage(image);
    }

    public EdsImage? DownloadStream()
    {

        Eds.CheckError(Eds.EdsCreateMemoryStream((long)this.Size, out nint stream));

        Eds.CheckError(Eds.EdsDownload(this.item, this.Size, stream));

        Eds.CheckError(Eds.EdsDownloadComplete(this.item));

        return null;
    }
}
