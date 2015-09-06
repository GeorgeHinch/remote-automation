using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Power;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LightsPage : Page
    {
        public LightsPage()
        {
            this.InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            EventHandler<Object> stupd = new EventHandler<object>(this.timer_Tick);
            timer.Tick += stupd;
            timer.Start();


            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/5e726fc9-2569-4915-9af1-e1493524adf5/switches/?access_token=385f1cb1-9d53-4828-9cc3-931087483137");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.UTF8.GetBytes(result);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var byteString = System.Text.Encoding.UTF8.GetString(bytes);
                    SwitchesDetails[] switchesDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<SwitchesDetails[]>(byteString);

                    for (int i = 0; i < switchesDetails.Length; i++)
                    {
                        Debug.WriteLine(switchesDetails[i].label);
                        Light1.Text = switchesDetails[i].label;
                    }
                }
            }
        }

        async void timer_Tick(object sender, object something)
        {
            //var wifiAdapters = await WiFiAdapter.FindAllAdaptersAsync();
            //var firstWifiAdapter = wifiAdapters[0]; // be more careful, check size, etc...
            //Debug.WriteLine(wifiAdapters[0]);       // again, check size, or look for your specific network
            //var rssi = firstWifiAdapter.NetworkReport.AvailableNetworks[0].NetworkRssiInDecibelMilliwatts;
            this.pwrIcon(this.getCurrentPowerSupplyStatus());
            // do whatever you want with your RSSI

        } 

        public PowerSupplyStatus getCurrentPowerSupplyStatus()
        {
            return Windows.System.Power.PowerManager.PowerSupplyStatus;
        }

        public void pwrIcon(PowerSupplyStatus status)
        {
            switch (status)
            {
                case PowerSupplyStatus.Inadequate:
                    //strPowerLineStatus = "Device is running on battery";
                    pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.Adequate:
                    //strPowerLineStatus = "Device is plugged in";
                    pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.NotPresent:
                    //strPowerLineStatus = "Power Status: Unknown";
                    pwrStateIcon.Text = $"";
                    break;
            }
        }

        public class SwitchesDetails
        {
            public String id { get; set; }
            public String label { get; set; }
            public String type { get; set; }
        }
    

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
