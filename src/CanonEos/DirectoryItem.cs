namespace CanonEos;

public abstract class DirectoryItem
{
    public abstract ulong Size { get; }

    public abstract bool IsFolder { get; }

    public abstract uint GroupID { get; }

    public abstract uint Option { get; }

    public abstract string Name { get; }

    public abstract EdsImageType Format { get; }

    public abstract DateTime DateTime { get; }

    public abstract EdsFileAttribute Attribute { get; set; }


    public abstract IEnumerable<DirectoryItem> DirectoryItems { get; }
    

    public abstract IEnumerable<DirectoryItem> Directories { get; }
   
    public abstract IEnumerable<DirectoryItem> Files { get; }

    public abstract void DownloadThumbnail(string filePath);

    public abstract void Download(string filePath);

    public abstract void Delete();

    public abstract EdsImage DownloadImage();

    public abstract EdsImage? DownloadStream();

}
