namespace CanonWpf.Controls;

public class Explorer : Control
{
    static Explorer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Explorer), new FrameworkPropertyMetadata(typeof(Explorer)));
    }

    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(Explorer));

    public object? Source
    {
        get => (object?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }



    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(Explorer),
        new UIPropertyMetadata(null, (d, e) => ((Explorer)d).OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.OldValue)));
    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        //if (newValue is not base IExplorerItem)
        this.InternalSource = newValue.Cast<object>().Select(i => new ExplorerItem((IExplorerItem)i)).ToList();
    }



    public static readonly DependencyProperty InternalSourceProperty = DependencyProperty.Register("InternalSource", typeof(IEnumerable), typeof(Explorer));

    public IEnumerable? InternalSource
    {
        get => (IEnumerable?)GetValue(InternalSourceProperty);
        set => SetValue(InternalSourceProperty, value);
    }












    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(Explorer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public object? SelectedItem
    {
        get => (object?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}
