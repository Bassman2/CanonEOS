using CommunityToolkit.Mvvm.ComponentModel;

namespace CanonWpf.ViewModel;

public abstract partial class TreeItemViewModel : ObservableObject
{
    public TreeItemViewModel()
    {
        this.IsExpanded = false;
    }

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private IEnumerable<object> children = [];

    [ObservableProperty]
    private bool isExpanded;

    partial void OnIsExpandedChanging(bool value)
    { }

    partial void OnIsExpandedChanged(bool value)
    {
        Update();
    }

    protected virtual void Update() { }

    protected void SetHasChilden(bool hasChildren)
    {
        if (hasChildren)
        {
            Children = [new Dummy()];
        }
        //else
        //{
        //    Children = null;
        //}
    }

    public partial class Dummy : TreeItemViewModel
    {
        public Dummy()
        {
            Name = "Dummy";
        }
    }
}
