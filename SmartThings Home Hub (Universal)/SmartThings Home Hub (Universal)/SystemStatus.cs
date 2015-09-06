using System;
using System.Collections.Generic;
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
                    //strPowerLineStatus = "Device is running on battery";
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.Adequate:
                    //strPowerLineStatus = "Device is plugged in";
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.NotPresent:
                    //strPowerLineStatus = "Power Status: Unknown";
                    //pwrStateIcon.Text = $"";
                    break;
            }
        }
    }
}
