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

        CanonSDK.EdsGetDirectoryItemInfo(item, out EdsDirectoryItemInfo info);

        this.Name = info.FileName;
    }

    public string Name { get; }

    public IEnumerable<DirectoryItem> DirectoryItems
    {
        get => CanonSDK.GetChildren(item).Select(i => new DirectoryItem(i));
    }
}
