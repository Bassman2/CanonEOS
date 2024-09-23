using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;
using System;

namespace CanonEos.CcApi;

public static class CcFinder
{
    public static IEnumerable<string>? FindCameras()
    {
        var addr = Dns.GetHostAddresses(string.Empty, AddressFamily.InterNetwork);

        IPAddress? gateway = GetDefaultGateway();




        if (gateway != null)
        {
            Ping ping = new Ping();

            byte[] addrBytes = gateway.GetAddressBytes();
            //for (int i = 2; i <= 255; i++)
            //{
            //    addrBytes[3] = (byte)i;
            //    IPAddress ad = new IPAddress(addrBytes);
            //    //Debug.WriteLine(ad);

            //    var res = ping.Send(ad, 100);
            //    Debug.WriteLine($"{ad} {res.Status} {res.RoundtripTime}");
            //}
            ConcurrentBag<IPAddress> values = new ConcurrentBag<IPAddress>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Parallel.For(2, 256, i =>
            //{
            //    addrBytes[3] = (byte)i;
            //    IPAddress ad = new IPAddress(addrBytes);
            //    var res = ping.SendPingAsync(ad, 100).Result;
            //    Debug.WriteLine($"{ad} {res.Status} {res.RoundtripTime}");
            //});

            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            for (int i = 2; i <= 255; i++)
            {
                addrBytes[3] = (byte)i;
                IPAddress ad = new IPAddress(addrBytes);
                pingTasks.Add(PingAsync(ad));
            }
            Task.WaitAll(pingTasks.ToArray());

            //Now you can iterate over your list of pingTasks

            stopWatch.Stop();
            Debug.WriteLine(stopWatch.Elapsed);

            //foreach (var pingTask in pingTasks.Where(t => t.Result.Status == IPStatus.Success).Where(t => IsCanonCamera(t.Result.Address)))
            //{
            //    Debug.WriteLine($"{pingTask.Result.Address} {pingTask.Result.RoundtripTime} {Dns.GetHostEntry(pingTask.Result.Address).HostName}");
            //}


            stopWatch.Start();

            List<Task<DevDesc>> devDescTasks = new List<Task<DevDesc>>();
            foreach (var pingTask in pingTasks.Where(t => t.Result.Status == IPStatus.Success))
            {
                devDescTasks.Add(CheckDevDesc(pingTask.Result.Address));
            }
            Task.WaitAll(devDescTasks.ToArray());

            stopWatch.Stop();
            Debug.WriteLine(stopWatch.Elapsed);


            foreach (var d in devDescTasks.Where(t => t.Result.IsCanonCamera))
            {
                Debug.WriteLine($"{d.Result.Address} {d.Result.CameraDevDesc?.Device?.ServiceList?[0].DeviceNickname} {d.Result.CameraDevDesc?.Device?.ServiceList?[0].AccessURL}");
            }
            //Uri upnpUri = new UriBuilder("http", host, 49152, "/upnp/CameraDevDesc.xml").Uri;
            //using HttpClient upnp = new HttpClient();

        }
        return null;
    }

    private static bool IsCanonCamera(IPAddress addr)
    {
        Uri upnpUri = new UriBuilder("http", addr.ToString(), 49152, "/upnp/CameraDevDesc.xml").Uri;
        using HttpClient upnp = new HttpClient();
        try
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(addr.ToString(), 49152);



            TcpClient client = new TcpClient();
            client.Connect(addr.ToString(), 49152);
            bool b = client.Connected;
            
            //System.Net.Sockets.Socket.
            HttpResponseMessage res = upnp.GetAsync(upnpUri).Result;
            return res.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Exception {addr}");
            return false;
        }
       
    }

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
