namespace CanonUtility.Controls;

public class CameraExplorer : Control
{
    static CameraExplorer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CameraExplorer), new FrameworkPropertyMetadata(typeof(CameraExplorer)));
    }

    public static readonly DependencyProperty CameraProperty =
        DependencyProperty.Register("Camera", typeof(Camera), typeof(CameraExplorer),
            new UIPropertyMetadata(null, //PropertyChangedCallback));
            // new FrameworkPropertyMetadata(null, PropertyChangedCallback));
            (o, e) => ((CameraExplorer)o).OnCameraChanged((Camera?)e.OldValue, (Camera?)e.NewValue)));

    public Camera? Camera
    {
        get => (Camera?)GetValue(CameraProperty);
        set => SetValue(CameraProperty, value);
    }

    

    protected virtual void OnCameraChanged(Camera? oldValue, Camera? newValue)
    {
        this.Volumes = newValue?.Volumes;    
    }

    



    public static readonly DependencyProperty VolumesProperty =
        DependencyProperty.Register("Volumes", typeof(IEnumerable<Volume>), typeof(CameraExplorer),
            new UIPropertyMetadata(null, PropertyChangedCallback));
    // new FrameworkPropertyMetadata(null, PropertyChangedCallback));
    //(o, e) => ((CameraExplorer)o).OnCameraChanged((Camera?)e.OldValue, (Camera?)e.NewValue)));

    public IEnumerable<Volume> Volumes
    { 
        get => (IEnumerable<Volume>) GetValue(VolumesProperty);
        set => SetValue(VolumesProperty, value);
    }

    static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
    }

    public static readonly DependencyProperty SelectedDirectoryItemProperty =
        DependencyProperty.Register("SelectedDirectoryItem", typeof(DirectoryItem), typeof(CameraExplorer), new UIPropertyMetadata(null, 
            (o, e) => ((CameraExplorer)o).OnSelectedDirectoryItemChanged((DirectoryItem?)e.OldValue, (DirectoryItem?)e.NewValue)));

    public DirectoryItem? SelectedDirectoryItem
    {
        get => (DirectoryItem?)GetValue(SelectedDirectoryItemProperty);
        set => SetValue(SelectedDirectoryItemProperty, value);
    }

    protected virtual void OnSelectedDirectoryItemChanged(DirectoryItem? oldValue, DirectoryItem? newValue)
    {
        this.SelectedDirectoryItemProperties = newValue?.Properties;
    }


    public static readonly DependencyProperty SelectedDirectoryItemPropertiesProperty =
        DependencyProperty.Register("SelectedDirectoryItemProperties", typeof(IEnumerable<Property>), typeof(CameraExplorer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public IEnumerable<Property>? SelectedDirectoryItemProperties
    {
        get => (IEnumerable<Property>?)GetValue(SelectedDirectoryItemProperty);
        set => SetValue(SelectedDirectoryItemProperty, value);
    }
}
