using NativeWifi;
using System;
using System.Text;


namespace SmartThings_Hub
{
    public class NetworkStatus
    {
        WlanClient wlan;

        public NetworkStatus()
        {
           wlan = new WlanClient();
        }

        public NetworkStatusResult refresh()
        {
            NetworkStatusResult toReturn = new NetworkStatusResult();
            if (wlan.Interfaces.Length == 0)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.ETHER;
                return toReturn;
            }
            WlanClient.WlanInterface wlanInterface = wlan.Interfaces[0];

            Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
            var sigStrength = wlanInterface.CurrentConnection.wlanAssociationAttributes.wlanSignalQuality;
            toReturn.Ssid = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
            if (sigStrength > 0 && sigStrength < 25)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.ONE;
            } else if (sigStrength >=25 && sigStrength < 50)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.TWO;
            } else if (sigStrength >= 50 && sigStrength < 75)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.THREE;
            } else if (sigStrength >=75)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.FOUR;
            } else if (sigStrength == 0)
            {
                toReturn.SigStrength = NetworkResultSignalStrength.ZERO;
            }
            return toReturn;

        }

        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }
    }
}
