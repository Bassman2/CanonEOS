namespace CanonWpf.Controls;

public interface IExplorerItem
{ 
    bool IsFolder { get; }

    bool IsInitialExpanded { get; }

    string Name { get; }

    ImageSource?  Icon { get; }

    bool HasFolders { get; }

    IEnumerable<IExplorerItem> Folders { get; }

    bool HasItems { get; }

    IEnumerable<IExplorerItem> Items { get; }
}
