using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;

namespace KRBAccounting.Web.Helpers
{
    public class IpHelper
    {

        public static string GetClientComputerName()
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine(ipProperties.HostName);

            foreach (NetworkInterface networkCard in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (GatewayIPAddressInformation gatewayAddr in networkCard.GetIPProperties().GatewayAddresses)
                {
                    var ips = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
                    Console.WriteLine("Information: ");
                    Console.WriteLine("Interface type: {0}", networkCard.NetworkInterfaceType.ToString());
                    Console.WriteLine("Name: {0}", networkCard.Name);
                    Console.WriteLine("Id: {0}", networkCard.Id);
                    Console.WriteLine("Description: {0}", networkCard.Description);
                    Console.WriteLine("Gateway address: {0}", gatewayAddr.Address.ToString());
                    Console.WriteLine("IP: {0}", System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString());
                    Console.WriteLine("Speed: {0}", networkCard.Speed);
                    Console.WriteLine("MAC: {0}", networkCard.GetPhysicalAddress().ToString());
                }
            }
            string IP = HttpContext.Current.Request.UserHostName;
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }

        public static string GetClientIp()
        {
            var compName = GetClientComputerName();
            string IP4Address = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(compName))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
        public static string GetClientIpName(string compName)
        {
           
            string IP4Address = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(compName))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
       
    }
}