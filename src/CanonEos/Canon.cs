
namespace CanonEos;

public sealed class Canon : IDisposable
{
    //public event CameraAddedEventHandler? CameraAdded;

    private EdCanon edCanon;
    private CcCanon ccCanon;
    private MergedObservableCollection<Camera> mergedCollection;

    public Canon()
    {
        edCanon = new EdCanon();
        ccCanon = new CcCanon();

        mergedCollection = new MergedObservableCollection<Camera>(edCanon.CamerasX, ccCanon.CamerasX);
    }

    public void Dispose()
    {
        edCanon.Dispose();
        ccCanon.Dispose();
    }

    //public IEnumerable<Camera> Cameras => edCanon.Cameras.Cast<Camera>().Concat(ccCanon.Cameras);
    
    public ObservableCollection<Camera> CamerasX => mergedCollection;

    public static Camera GetCamera(string host) => CcCanon.GetCamera(host);

    
}
