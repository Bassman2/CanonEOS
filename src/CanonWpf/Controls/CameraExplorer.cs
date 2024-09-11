namespace CanonWpf.Controls;

public class CameraExplorer : Control
{
    static CameraExplorer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CameraExplorer), new FrameworkPropertyMetadata(typeof(CameraExplorer)));
    }

    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(CameraExplorer));

    public object? Source
    {
        get => (object?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(DirectoryItem), typeof(CameraExplorer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public DirectoryItem? SelectedItem
    {
        get => (DirectoryItem?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    
}
