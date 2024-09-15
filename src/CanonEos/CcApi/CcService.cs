namespace CanonEos.CcApi;

internal class CcService : IDisposable
{
    private readonly Uri defaultHost = new Uri("http://fritz.box");
    private readonly HttpClientHandler? handler;
    private HttpClient client;
    //private readonly string sessionId;

    public CcService()
    {
        // connect
        this.handler = new HttpClientHandler
        {
            CookieContainer = new System.Net.CookieContainer(),
            UseCookies = true
        };
        this.client = new HttpClient(this.handler)
        {
            BaseAddress = this.defaultHost,
            Timeout = new TimeSpan(0, 2, 0)
        };
    }

    public void Dispose()
    {
        this.client.Dispose();
    }

    public DeviceInformation GetDeviceInformation()
    {
        string res = this.client.GetStringAsync("").Result;
        return new DeviceInformation();
    }
}
