using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
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

        public void georgeRoomLamp_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=switch&device=2c9650de-25da-4f1c-8ea0-51ca3c984007&command=toggle&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79");
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

        public void loadDevices()
        {
            string app = getApp();
            string token = getToken();

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "data?access_token=" + token;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        List<Button> deviceButtonList = new List<Button>();

                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device" && sth.type == "switch")
                            {
                                Button btn = new Button();
                                StackPanel sp = new StackPanel();
                                TextBlock tbIcon = new TextBlock();
                                TextBlock tbName = new TextBlock();
                                TextBlock tbType = new TextBlock();


                                #region Create icon textblock
                                tbIcon.Text = WebUtility.HtmlDecode("&#60032;");
                                if (sth.value == "on")
                                {
                                    tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                                } else { tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 89, 89, 89)); }
                                tbIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                                tbIcon.TextAlignment = TextAlignment.Center;
                                tbIcon.FontSize = 48;
                                tbIcon.Margin = new Thickness(0, 0, 0, 15);
                                #endregion

                                #region Create name textblock
                                tbName.Text = sth.name;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.Margin = new Thickness(0, 0, 0, 5);
                                tbName.Foreground = new SolidColorBrush(Color.FromArgb(255, 89, 89, 89));
                                #endregion

                                #region Create type textblock
                                tbType.Text = sth.type;
                                tbType.TextAlignment = TextAlignment.Center;
                                tbType.MaxLines = 2;
                                tbType.TextWrapping = TextWrapping.Wrap;
                                tbType.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                                #endregion


                                #region Add textblocks to stackpanel
                                sp.Children.Add(tbIcon);
                                sp.Children.Add(tbName);
                                sp.Children.Add(tbType);
                                #endregion

                                #region Add stackpanel to button
                                btn.Content = sp;
                                // Width="175" Click="brelandRoomLight_Click" Background="{x:Null}" BorderBrush="{x:Null}" VerticalAlignment="Top"
                                btn.Width = 175;
                                btn.Click += new RoutedEventHandler(toggleLight);
                                btn.Background = null;
                                btn.BorderBrush = null;
                                btn.VerticalAlignment = VerticalAlignment.Top;
                                btn.Tag = sth.device;
                                #endregion

                                #region Add button to list
                                deviceButtonList.Add(btn);
                                #endregion
                            }
                        }
                    }
                }
            }
        }

        #region Button tapped handler to toggle lights
        public void toggleLight(object sender, RoutedEventArgs e)
        {
            Button sendr = (Button)sender;
            string app = getApp();
            string device = sendr.Tag.ToString();
            string command = lightStatus(device);
            string token = getToken();

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=switch&device=" + device + "&command=" + command + "&access_token=" + token;
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                rqstMsg);
            HttpClient client = new HttpClient();
            client.SendAsync(request);
        }
        #endregion

        #region Checks the on/off status of light
        public string lightStatus(string sender)
        {
            string app = getApp();
            string token = getToken();
            string command;
            string deviceVal = "";

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "data?access_token=" + token; 

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device")
                            {
                                if (sth.device == sender)
                                {
                                    deviceVal = sth.value;
                                }
                            }
                        }
                    }
                }
            }

            if (deviceVal == "off")
            {
                command = "on";
            }
            else { command = "off"; }

            return command;
        }
        #endregion

        #region Gets the SmartThings app ID
        public string getApp()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string app = "";

            /* Load SmartThings App ID */
            if (roamingSettings.Values["stAppID"] == null)
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                app = roamingSettings.Values["stAppID"].ToString();
            }

            return app;
        }
        #endregion

        #region Gets the SmartThings app token
        public string getToken()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string token = "";

            /* Load SmartThings Access Token */
            if (roamingSettings.Values["stToken"] == null)
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                token = roamingSettings.Values["stToken"].ToString();
            }

            return token;
        }
        #endregion

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
