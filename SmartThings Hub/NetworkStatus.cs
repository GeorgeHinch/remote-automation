using NativeWifi;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartThings_Hub
{
    public class NetworkStatus
    {

        public NetworkStatus()
        {
           var wlan = new WlanClient();

            Collection<String> connectedSsids = new Collection<string>();

            foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
            {
                Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                connectedSsids.Add(new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength)));
            }

            /*WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                foreach (Wlan.WlanAvailableNetwork network in networks)
                {
                    Console.WriteLine("Found network with SSID {0} and Siqnal Quality {1}.", GetStringForSSID(network.dot11Ssid), network.wlanSignalQuality);
                }
            }*/
        }

        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }
    }
}