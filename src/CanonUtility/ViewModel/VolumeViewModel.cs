namespace CanonUtility.ViewModel;

public partial class VolumeViewModel : TreeItemViewModel
{
    private Volume volume;
    public VolumeViewModel(Camera camera, Volume volume)
    {
        this.volume = volume;
        this.Name = volume.Name;

        SetHasChilden(true);
    }

    protected override void Update() 
    { 
        this.Children = volume.Directories?.Select(d => new DirectoryViewModel(d)).ToList() ?? [];
    }
}
