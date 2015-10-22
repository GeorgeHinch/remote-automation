using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Devices;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.System;
using Windows.System.Power;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace SmartThings_Home_Hub__Universal_
{
    class SystemStatus
    {
        public void pwrIcon(PowerSupplyStatus status)
        {
            switch (status)
            {
                case PowerSupplyStatus.Inadequate:
                    Debug.WriteLine("PowerSupply inadequete.");
                    //strPowerLineStatus = "Device is running on battery";
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.Adequate:
                    Debug.WriteLine("PowerSupply adequete.");
                    //strPowerLineStatus = "Device is plugged in";
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.NotPresent:
                    Debug.WriteLine("There should be zero reasons you're seeing this.");
                    //strPowerLineStatus = "Power Status: Unknown";
                    //pwrStateIcon.Text = $"";
                    break;
            }
        }

        public PowerSupplyStatus pwrState()
        {
            var status = PowerManager.PowerSupplyStatus;
            Debug.WriteLine(status.ToString());
            return status;
        }

        public void netInterface()
        {
            var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            if (profile != null)
            {
                var interfaceType = profile.NetworkAdapter.IanaInterfaceType;

                // 71 is WiFi & 6 is Ethernet(LAN)
                if (interfaceType == 71)
                {
                    Debug.WriteLine("Wifi found!");
                }
                // 71 is WiFi & 6 is Ethernet(LAN)
                else if (interfaceType == 6)
                {
                    Debug.WriteLine("Ethernet found!");
                }
                // 243 & 244 is 3G/Mobile
                else if (interfaceType == 243 || interfaceType == 244)
                {
                    Debug.WriteLine("Mobile network found!");
                }
                else
                {
                    Debug.WriteLine("What the fuck kind of network do you have?");
                }
            }
        }

        public void netAP()
        {
            var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            var prof = Windows.Networking.Connectivity.NetworkInformation.GetConnectionProfiles();
            var inter = profile.GetNetworkNames();

            Debug.WriteLine(inter.ToString());
        }

        /* public WlanConnectionProfileDetails getSSID()
        {
            var status = WlanConnectionProfileDetails.
        } */
    }
}
