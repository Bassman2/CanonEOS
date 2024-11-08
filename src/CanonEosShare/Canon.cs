


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

        mergedCollection = new MergedObservableCollection<Camera>(edCanon.Cameras, ccCanon.Cameras);
    }

    public void Dispose()
    {
        edCanon.Dispose();
        ccCanon.Dispose();
    }
    
    public ObservableCollection<Camera> Cameras => mergedCollection;

    public static Camera GetCamera(string host) => CcCanon.GetCamera(host);

    
}
