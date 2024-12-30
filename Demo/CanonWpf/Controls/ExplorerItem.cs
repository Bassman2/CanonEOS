namespace CanonWpf.Controls;

public class ExplorerItem
{
    private static readonly BitmapImage? folderImage = new(new Uri("pack://application:,,,/CanonWpf;component/Images/folder16.png"));
    private static readonly BitmapImage? fileImage = new (new Uri("pack://application:,,,/CanonWpf;component/Images/file16.png"));
    private static readonly ExplorerItem dummy = new();

    private readonly IExplorerItem item;

    public ExplorerItem() : this(new Dummy())
    { }

    public ExplorerItem(IExplorerItem item)
    {
        this.item = item;
        this.isExpanded = item.IsInitialExpanded;
        if (isExpanded)
        {
            Folders = item.Folders.Select(f => new ExplorerItem(f)).ToList();
        }
        else
        {
            Folders = item.HasFolders ? [dummy] : [];
        }
    }

    public string Name => item.Name;

    public ImageSource? Icon => item.Icon ?? (item.IsFolder ? folderImage : fileImage);

    public IEnumerable<ExplorerItem>? Folders { get; private set; }


    private bool isLoaded = false;
    private bool isExpanded;
    public bool IsExpanded
    {
        get => isExpanded;
        set
        {
            if (value && !isLoaded)
            {
                
            }

            isExpanded = value;
        }
    }

    public IEnumerable<object> Items => item.Items;


    
    public class Dummy : IExplorerItem
    {
        public bool IsFolder => true;

        public bool IsInitialExpanded => false;

        public string Name => "Dummy";

        public ImageSource? Icon => null;

        public bool HasFolders => false;

        public IEnumerable<IExplorerItem> Folders => [];

        public bool HasItems => false;

        public IEnumerable<IExplorerItem> Items => [];
    }
}
