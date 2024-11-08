namespace CanonEos;

public abstract class EosFile : EosFileSystemItem
{
    public abstract ulong Size { get; }

    public abstract void DownloadThumbnail(string filePath);

    public abstract void Download(string filePath);



    //public abstract EdsImage DownloadImage();

    //public abstract EdsImage? DownloadStream();

}
