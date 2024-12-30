namespace CanonEos.EdSdk;

internal class EdDirectory : EosDirectory
{
    private static readonly DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    private readonly nint item;

    internal EdDirectory(nint item, EdsDirectoryItemInfo info)
    {
        this.item = item;

        //Eds.CheckError(Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info));
        
        this.Name = info.FileName;
        this.FullName = info.FileName;
        //this.Size = info.Size64;
        //this.IsFolder = info.IsFolder;
        //this.GroupID = info.GroupID;
        //this.Option = info.Option;
        //
        //this.Format = (EdsImageType)(((int)info.Format) & 0xf);
        //this.DateTime = unixDateTime.AddSeconds(info.DateTime).ToLocalTime();

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

    public override string Name { get; }

    public override string FullName { get; }

    public override DateTime CreationTime { get; }

    public override bool IsFolder => true;

    public override void Delete()
    {
        Eds.EdsDeleteDirectoryItem(this.item);
    }

    public override void Refresh()
    { }

    





    //public override EdsFileAttribute Attribute
    //{
    //    get
    //    {
    //        Eds.EdsGetAttribute(this.item, out EdsFileAttribute attr);
    //        return attr;
    //    }
    //    set
    //    {
    //        Eds.EdsSetAttribute(this.item, value);
    //    }
    //}

    //private EosFileSystemItem FileSystemFactory(nint item)
    //{
    //    Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
    //    return info.IsFolder ? new EdDirectory(item, info) : new EdFile(item, info); 
        
    //}

    public override IEnumerable<EosFileSystemItem> FileSystemItems
    {
        get //=> Eds.GetChildren(item).Select(i => FileSystemFactory(i));
        {
            foreach (nint item in Eds.GetChildren(item))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                yield return info.IsFolder ? new EdDirectory(item, info) : new EdFile(item, info);
            }
        }
    }

    public override IEnumerable<EdDirectory> Directories
    {
        get //=> FileSystemItems.Where(d => d is EdDirectory).Cast<EdDirectory>();
        {
            foreach (nint item in Eds.GetChildren(item))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                if (info.IsFolder)
                {
                    yield return new EdDirectory(item, info);
                }
            }
        }
    }

    public override IEnumerable<EdFile> Files
    {
        get //=> FileSystemItems.Where(d => d is EdFile).Cast<EdFile>();
        { 

            foreach (nint item in Eds.GetChildren(item))
            {
                Eds.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);
                if (!info.IsFolder)
                {
                    yield return new EdFile(item, info);
                }
            }
        }
    }
}
