using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;
using System;

namespace CanonEos.CcApi;

public static class CcFinder
{
    public static IEnumerable<CameraDevDesc>? FindCameras()
    {
        //var addr = Dns.GetHostAddresses(string.Empty, AddressFamily.InterNetwork);

        IPAddress? gateway = GetDefaultGateway();
        if (gateway != null)
        {
            byte[] addrBytes = gateway.GetAddressBytes();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            for (int i = 2; i <= 255; i++)
            {
                addrBytes[3] = (byte)i;
                IPAddress ad = new IPAddress(addrBytes);
                pingTasks.Add(PingAsync(ad));
            }
            Task.WaitAll(pingTasks.ToArray());

            //stopWatch.Stop();
            //Debug.WriteLine(stopWatch.Elapsed);

            //stopWatch.Start();

            List<Task<DevDesc>> devDescTasks = new List<Task<DevDesc>>();
            foreach (var pingTask in pingTasks.Where(t => t.Result.Status == IPStatus.Success))
            {
                devDescTasks.Add(CheckDevDesc(pingTask.Result.Address));
            }
            Task.WaitAll(devDescTasks.ToArray());

            stopWatch.Stop();
            Debug.WriteLine($"FindCameras: {stopWatch.Elapsed}");

            return devDescTasks.Where(t => t.Result.IsCanonCamera).Select(t => t.Result.CameraDevDesc!);
        }
        return null;
    }

    //private static bool IsCanonCamera(IPAddress addr)
    //{
    //    Uri upnpUri = new UriBuilder("http", addr.ToString(), 49152, "/upnp/CameraDevDesc.xml").Uri;
    //    using HttpClient upnp = new HttpClient();
    //    try
    //    {
    //        Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
    //        socket.Connect(addr.ToString(), 49152);



    //        TcpClient client = new TcpClient();
    //        client.Connect(addr.ToString(), 49152);
    //        bool b = client.Connected;
            
    //        //System.Net.Sockets.Socket.
    //        HttpResponseMessage res = upnp.GetAsync(upnpUri).Result;
    //        return res.IsSuccessStatusCode;
    //    }
    //    catch (Exception ex)
    //    {

    //        Debug.WriteLine($"Exception {addr}");
    //        return false;
    //    }
       
    //}

    private class DevDesc(IPAddress addr, CameraDevDesc? devDesc = null)
    {
        public IPAddress? Address { get; } = addr;
        public CameraDevDesc? CameraDevDesc { get; } = devDesc;
        public bool IsCanonCamera => CameraDevDesc is not null;
    }

    private static Task<DevDesc> CheckDevDesc(IPAddress addr)
    {
        return Task<DevDesc>.Run<DevDesc>(() =>
        {
            try
            {
                Uri upnpUri = new UriBuilder("http", addr.ToString(), 49152, "/upnp/CameraDevDesc.xml").Uri;
                using HttpClient upnp = new HttpClient() { Timeout = new TimeSpan(0, 0, 0, 0, 200) };

                string text = upnp.GetStringAsync(upnpUri).Result;

                XmlSerializer serializer = new XmlSerializer(typeof(CameraDevDesc));
                CameraDevDesc? cameraDevDesc = (CameraDevDesc?)serializer.Deserialize(new StringReader(text));
                return new DevDesc(addr, cameraDevDesc);
            }
            catch
            {
                return new DevDesc(addr);
            }
        });

    }
    
    private static Task<PingReply> PingAsync(IPAddress address)
    {
        //Ping ping = new Ping();
        //Debug.WriteLine($"A {address}");

        return new Ping().SendPingAsync(address, 200);
        //Debug.WriteLine($"B {address} {res.Result.Address} {res.Result.RoundtripTime}");
        //return res;


        //var tcs = new TaskCompletionSource<PingReply>();
        //Ping ping = new Ping();
        //ping.PingCompleted += (obj, sender) =>
        //{
        //    tcs.SetResult(sender.Reply);
        //};
        //ping.SendAsync(address, new object());
        //return tcs.Task;
    }

    private static IPAddress? GetDefaultGateway()
    {
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            Debug.WriteLine($"{networkInterface.Name} {networkInterface.Id} {networkInterface.OperationalStatus} {networkInterface.NetworkInterfaceType}");
        }

        var addr = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(n => GetGateway(n)).FirstOrDefault();
        return addr;
    }

    private static IPAddress? GetGateway(NetworkInterface networkInterface)
    {
        foreach (var gateway in networkInterface.GetIPProperties().GatewayAddresses)
        {
            Debug.WriteLine($"{gateway.Address} {gateway.Address.AddressFamily}");
        }

        var addr = networkInterface.GetIPProperties().GatewayAddresses.Select(a => a.Address).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
        return addr;
    }

    //private static Task<double Ping(IPAddress addr)
    //{
    //    var reply = new Ping().SendPingAsync(addr);
    //    if (reply != null)
    //        return reply.RoundtripTime;
    //    throw new Exception("denied");
    //}
}
