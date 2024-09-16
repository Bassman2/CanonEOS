using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace CanonEos.CcApi.Internal;

internal static class Network
{
    //private static readonly int timeout = 4000;

    private static IEnumerable<IPAddress> NetworkGateways()
    {
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in adapters)
        {
            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
            if (addresses.Count > 0)
            {
                Console.WriteLine(adapter.Description);
                foreach (GatewayIPAddressInformation address in addresses)
                {
                    Console.WriteLine("  Gateway Address ......................... : {0}", address.Address.ToString());
                    yield return address.Address;
                }
                Console.WriteLine();
            }
        }
    }

    private static IEnumerable<IPAddress> NetworkDevices()
    {
        foreach (IPAddress gateway in NetworkGateways())
        {
            byte[] addr = gateway. GetAddressBytes();
            for (byte num = 2; num <= 255; num++)
            {
                addr[3] = (byte)num;
                yield return new IPAddress(addr);
            }
        }
    }

    //private static List<IPAddress> devices = new List<IPAddress>();
    //private static Task? async PingDeviceAsync(IPAddress addr)
    //{
    //    Ping ping = new Ping();
    //    await ping.SendPingAsync(addr, timeout).ContinueWith(p => { if (p.Result.Status == IPStatus.Success) { devices.Add(addr) } });
    //}


    //public static IEnumerable<IPAddress> ActiveDevices()
    //{
    //    devices.Clear();
    //    List<IPAddress> devs = NetworkDevices().ToList();

    //    List<Task> tasks = devs.Select(d => await PingDeviceAsync(d));
    //    Task.WaitAll(tasks.ToArray());
    //}
}
