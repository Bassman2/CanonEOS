using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CanonEos.CcApi;

public static class CcFinder
{
    private const int timeout = 400;

    public static IEnumerable<CameraDevDesc>? FindCameras()
    {
        IPAddress? gateway = GetDefaultGateway();
        if (gateway != null)
        {
            var pingList = FindActiveNetworkDevices(gateway);
            return FindCanonDevices(pingList);
        }
        return null;
    }

    private static IEnumerable<IPAddress> FindActiveNetworkDevices(IPAddress gateway)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var pingTasks = GetIPAddressRange(gateway).Select(a => PingAsync(a)).ToArray();
        Task.WaitAll(pingTasks);

        stopWatch.Stop();
        Debug.WriteLine($"FindActiveNetworkDevices: {stopWatch.Elapsed}");

        var list = pingTasks.Where(p => p.Result.Status == IPStatus.Success).Select(p => p.Result.Address).ToList();

        foreach (var p in list)
        {
            Debug.WriteLine($"Active Ping {p}");
        }

        return list;
    }

    private static IEnumerable<CameraDevDesc> FindCanonDevices(IEnumerable<IPAddress> addresses)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var devDescTasks = addresses.Select(a => CheckDevDesc(a)).ToArray();
        Task.WaitAll(devDescTasks);

        stopWatch.Stop();

        Debug.WriteLine($"FindCanonDevices: {stopWatch.Elapsed}");

        var list = devDescTasks.Where(t => t.Result.IsCanonCamera).Select(t => t.Result.CameraDevDesc!).ToList();

        foreach (var d in list)
        {
            Debug.WriteLine($"DevDesc {d.Device!.ServiceList![0].DeviceNickname}");
        }
        return list;
    }

    private static IEnumerable<IPAddress> GetIPAddressRange(IPAddress address)
    {
        byte[] addrBytes = address.GetAddressBytes();
        for (int i = 2; i <= 255; i++)
        {
            addrBytes[3] = (byte)i;
            yield return new IPAddress(addrBytes);
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
                using HttpClient upnp = new HttpClient() { Timeout = new TimeSpan(0, 0, 0, 0, timeout) };

                string text = upnp.GetStringAsync(upnpUri).Result;

                XmlSerializer serializer = new XmlSerializer(typeof(CameraDevDesc));
                CameraDevDesc? cameraDevDesc = (CameraDevDesc?)serializer.Deserialize(new StringReader(text));
                Debug.WriteLine($"******************************** {addr}");
                return new DevDesc(addr, cameraDevDesc);
            }
            catch
            {
                Debug.WriteLine($"Failed {addr}");
                return new DevDesc(addr);
            }
        });

    }
    
    private static Task<PingReply> PingAsync(IPAddress address) => new Ping().SendPingAsync(address, timeout);
        

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
}
