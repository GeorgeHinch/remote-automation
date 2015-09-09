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
                        ;// Light1.Text = switchesDetails[i].label;
                    }
                }
            }
        }

        /// Lights
        /// 
        public static void brelandRoomLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void brelandBathLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void brelandLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void entryLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void georgeBathLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);
           
        }

        public static void georgeRoomLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void hallwayLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void kitchenLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void kitchenBarLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void livingRoomCouchLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public static void livingRoomLight_Click(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

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
