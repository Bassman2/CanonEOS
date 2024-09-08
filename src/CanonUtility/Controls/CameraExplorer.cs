using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanonUtility.Controls;

/// <summary>
/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
///
/// Step 1a) Using this custom control in a XAML file that exists in the current project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:CanonUtility.Controls"
///
///
/// Step 1b) Using this custom control in a XAML file that exists in a different project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is 
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:CanonUtility.Controls;assembly=CanonUtility.Controls"
///
/// You will also need to add a project reference from the project where the XAML file lives
/// to this project and Rebuild to avoid compilation errors:
///
///     Right click on the target project in the Solution Explorer and
///     "Add Reference"->"Projects"->[Browse to and select this project]
///
///
/// Step 2)
/// Go ahead and use your control in the XAML file.
///
///     <MyNamespace:CameraExplorer/>
///
/// </summary>
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

    public Camera Camera
    {
        get => (Camera)GetValue(CameraProperty);
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
        DependencyProperty.Register("SelectedDirectoryItem", typeof(DirectoryItem), typeof(CameraExplorer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public DirectoryItem? SelectedDirectoryItem
    {
        get => (DirectoryItem?)GetValue(SelectedDirectoryItemProperty);
        set => SetValue(SelectedDirectoryItemProperty, value);
    }



}
