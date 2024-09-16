using System.Net;

namespace CanonEos.CcApi;

internal class CcService : IDisposable
{
    private readonly Uri defaultHost = new Uri("http://fritz.box");
    private readonly HttpClientHandler? handler;
    private HttpClient client;
    //private readonly string sessionId;

    public CcService(Uri url)
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

        // connect
        this.handler = new HttpClientHandler
        {
            CookieContainer = new System.Net.CookieContainer(),
            UseCookies = true
        };
        this.client = new HttpClient(this.handler)
        {
            BaseAddress = url,
            Timeout = new TimeSpan(0, 2, 0)
        };

        string res = this.client.GetStringAsync("ccapi").Result;
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
