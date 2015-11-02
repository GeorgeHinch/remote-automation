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
        public void brelandRoomLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=dimmer&device=43447e8e-9709-4445-8dd7-7c0e46fa149f&command=toggle&value=10&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void brelandBathLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=dimmer&device=08760254-cf3e-4ea1-a625-ec0822177d07&command=toggle&value=10&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void brelandLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=f04af812-1a35-4106-ba8a-04eedbf10c79&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void entryLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=a48055ea-da98-490d-9229-d9524dcda6b3&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void georgeBathLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=dimmer&device=765c8e03-2022-4c34-af6b-3cec3e44afe3&command=toggle&value=10&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);
           
        }

        public void georgeRoomLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void hallwayLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=7066e4f4-ef7a-4254-8140-306a2e36b8e0&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void kitchenLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=90270527-f60e-4382-8e5c-c1db05b3ae58&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void kitchenBarLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=dimmer&device=395d668e-1f0a-4321-b4fb-9a273c32322d&command=toggle&value=10&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void livingRoomCouchLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=7a609c91-5775-4bf1-a136-f7798941b20b&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

        public void livingRoomLight_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=dimmer&device=3a7e702b-4cc7-496c-9302-2475c4cff667&command=toggle&value=10&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
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
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.Adequate:
                    //strPowerLineStatus = "Device is plugged in";
                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.NotPresent:
                    //strPowerLineStatus = "Power Status: Unknown";
                    //pwrStateIcon.Text = $"";
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
