namespace CanonEos.CcApi;

public class CcDirectoryItem : DirectoryItem
{
    public CcDirectoryItem()
    {
        this.Name = "";
    }
    public override string Name { get; }

    public override ulong Size { get; }

    public override bool IsFolder { get; }

    public override uint GroupID { get; }

    public override uint Option { get; }


    public override EdsImageType Format { get; }

    public override DateTime DateTime { get; }

    public override EdsFileAttribute Attribute { get; set; }


    public override IEnumerable<CcDirectoryItem> DirectoryItems 
    { 
        get => []; 
    }


    public override IEnumerable<CcDirectoryItem> Directories { get => [];  }

    public override IEnumerable<CcDirectoryItem> Files { get => [];  }

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
