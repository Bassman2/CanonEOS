using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;

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

            foreach (var pingTask in pingTasks.Where(t => t.Result.Status == IPStatus.Success))
            {
                Debug.WriteLine($"{pingTask.Result.Address} {pingTask.Result.RoundtripTime} {Dns.GetHostEntry(pingTask.Result.Address).HostName}");
            }

        }
        return null;
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
