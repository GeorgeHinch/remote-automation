using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking.Connectivity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NetworkSettings : Page
    {
        public NetworkSettings()
        {
            this.InitializeComponent();

            netInterface();
            netIP();
            netInterfaceList();
            netStatusText();
        }

        public static string connectionLabel;

        public static string ipLabel;

        public static ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();

        public void netInterface()
        {
            if (connectionProfile != null)
            {
                var interfaceType = connectionProfile.NetworkAdapter.IanaInterfaceType;

                // 71 is WiFi & 6 is Ethernet(LAN)
                if (interfaceType == 71)
                {
                    connectionLabel = "wifi";
                }
                // 71 is WiFi & 6 is Ethernet(LAN)
                else if (interfaceType == 6)
                {
                    connectionLabel = "ethernet";
                }
                // 243 & 244 is 3G/Mobile
                else if (interfaceType == 243 || interfaceType == 244)
                {
                    connectionLabel = "3G/LTE";
                }
                else
                {
                    connectionLabel = "unknown";
                }
            }
        }

        public void netIP()
        {
            var Host = NetworkInformation.GetHostNames().Last();
            IPAddress address = IPAddress.Parse(Host.DisplayName);

            ipLabel = address.ToString();
        }

        public void netStatusText()
        {
            if (connectionLabel == "unkown")
            {
                netStatusTextBox.Text = "No networking hardware found.";
            }
            else
            {
                netStatusTextBox.Text = "This devices is connected to the network via " + connectionLabel + " and assigned the IP address " + ipLabel + ".";
            }
        }

        public void netInterfaceList()
        {
            if (connectionLabel == "wifi")
            {
                netInterfaceTextBox.Visibility = Visibility.Collapsed;

                sigWifiStackpanel.Visibility = Visibility.Visible;
                sigWifiTextblock.Text = connectionProfile.WlanConnectionProfileDetails.GetConnectedSsid();
            }
            else
            {
                netInterfaceTextBox.Visibility = Visibility.Visible;
                netInterfaceTextBox.Text = "No wireless device found.";
            }
        }
    }
}
