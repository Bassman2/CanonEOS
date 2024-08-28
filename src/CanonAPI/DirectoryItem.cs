using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonAPI;

public class DirectoryItem
{
    private readonly nint item;

    internal DirectoryItem(nint item)
    {
        this.item = item;

        EdsNativeLib.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);

        this.Name = info.FileName;
        this.IsFolder = info.IsFolder;
    }

    public string Name { get; }

    public bool IsFolder { get; }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => EdsNativeLib.GetChildren(item).Select(i => new DirectoryItem(i));
    }

    public IEnumerable<DirectoryItem> Directories
    {
        get => DirectoryItems.Where(d => d.IsFolder);
    }

    public IEnumerable<DirectoryItem> Files
    {
        get => DirectoryItems.Where(d => !d.IsFolder);
    }
}
