using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SmartThings_Home_Hub__Universal_.Controls
{
    public sealed partial class NotifBarTray : UserControl
    {
        public NotifBarTray()
        {
            this.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            EventHandler<Object> handlr = new EventHandler<object>(this.timer_Tick);
            timer.Tick += handlr;
            timer.Start();
        }

        async void timer_Tick(object sender, object something)
        {
            this.pwrIcon(this.getCurrentPowerSupplyStatus());
            this.sigIcon();

        }

        #region Determines power status
        public PowerSupplyStatus getCurrentPowerSupplyStatus()
        {
            return PowerManager.PowerSupplyStatus;
        }

        public void pwrIcon(PowerSupplyStatus status)
        {
            switch (status)
            {
                case PowerSupplyStatus.Inadequate:
                    pwrStateIcon.Text = $"";
                    pwrStateTextBlock.Text = "Inadequate power supply. Check state of the power source before the device enters standby mode.";
                    break;
                case PowerSupplyStatus.Adequate:
                    pwrStateIcon.Text = $"";
                    pwrStateTextBlock.Text = "The device is plugged into a power source.";
                    break;
                case PowerSupplyStatus.NotPresent:
                    pwrStateIcon.Text = $"";
                    pwrStateTextBlock.Text = "The device does not detect a power source. Verify that all cables are securely connected.";
                    break;
            }
        }
        #endregion

        #region Determines network status
        public async Task sigIcon()
        {
            Tuple<string, string, string> conProfile = this.netInterface(this.getCurrentConnectionProfile());

            sigStrengthIcon.Text = conProfile.Item2;
            sigStrengthTextblock.Text = "This devices is connected to the network via " + conProfile.Item1 + ".";

            #region If the network type is wifi
            if (conProfile.Item1 == "wifi")
            {
                sigWifiStackpanel.Visibility = Visibility.Visible;
                sigWifiTextblock.Text = conProfile.Item3;

                var wifiAdapters = await WiFiAdapter.FindAllAdaptersAsync();
                var firstWifiAdapter = wifiAdapters[0]; // be more careful, check size, etc...
                var rssi = firstWifiAdapter.NetworkReport.AvailableNetworks[0].NetworkRssiInDecibelMilliwatts;

                #region Conrects dBm to icon
                if (rssi >= -67)
                {
                    sigStrengthIcon.Text = "";
                }
                else if (rssi < -67 && rssi >= -70)
                {
                    sigStrengthIcon.Text = "";
                }
                else if (rssi < -70 && rssi >= -80)
                {
                    sigStrengthIcon.Text = "";
                }
                else if (rssi < -80)
                {
                    sigStrengthIcon.Text = "";
                }
                else
                {
                    sigStrengthIcon.Text = "";
                }
                #endregion
            }
            #endregion
        }

        public ConnectionProfile getCurrentConnectionProfile()
        {
            return NetworkInformation.GetInternetConnectionProfile();

        }

        public Tuple<string, string, string> netInterface(ConnectionProfile connectionProfile)
        {
            if (connectionProfile != null)
            {
                var interfaceType = connectionProfile.NetworkAdapter.IanaInterfaceType;

                // 71 is WiFi & 6 is Ethernet(LAN)
                if (interfaceType == 71)
                {
                    var interfaceSSID = connectionProfile.WlanConnectionProfileDetails.GetConnectedSsid();
                    return Tuple.Create("wifi", "", interfaceSSID.ToString());
                }
                // 71 is WiFi & 6 is Ethernet(LAN)
                else if (interfaceType == 6)
                {
                    return Tuple.Create("ethernet", "", "");
                }
                // 243 & 244 is 3G/Mobile
                else if (interfaceType == 243 || interfaceType == 244)
                {
                    return Tuple.Create("3G/LTE", "", "");
                }
                else
                {
                    return Tuple.Create("unknown", "", "");
                }
            }

            return null;
        }

        #endregion

        #region Determines and sets volume & now playing information
        private void volSoundIconBtn_Click(object sender, RoutedEventArgs e)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/data?access_token=" + token;

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
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub[]));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        #region Gets and sets album info from JSON
                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device" && sth.type == "music")
                            {
                                string trackDescription = (string)sth.trackDescription;

                                if (trackDescription.Before(" - ") != songTitle.Text)
                                {
                                    string song = trackDescription.Before(" - ");
                                    string artist = trackDescription.After(" - ");

                                    if (song.Trim() != "" && artist.Trim() != "")
                                    {
                                        songTitle.Text = song;
                                        songArtist.Text = artist;

                                        string albumArt = Spotify_AlbumFetch.fetchSpotifyArtwork(song, artist);

                                        if (albumArt != null)
                                        {
                                            songArt.Source = new BitmapImage(new Uri(albumArt, UriKind.Absolute));
                                        }
                                    }
                                    else { songTitle.Text = trackDescription; }
                                }

                                VolumeSlider.Value = (double)sth.level;

                                if (sth.status == "paused")
                                {
                                    PlayPauseButton.IsChecked = false;
                                }
                                else
                                {
                                    PlayPauseButton.IsChecked = true;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        private void Volume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine("Change: " + e.NewValue + " |");
            SmartThingsAPI_Actions.performAction("music", null, "level", e.NewValue, true);
        }

        private void PlayPauseButton_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("E: " + e.OriginalSource + " |");
            if (sender.ToString() == "Windows.UI.Xaml.Controls.Primitives.ToggleButton")
            {
                SmartThingsAPI_Actions.performAction("music", null, "play", 0, true);
            }
            PlayPauseButton.Content = "";
        }

        private void PlayPauseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Sender: " + sender.ToString() + " |");
            SmartThingsAPI_Actions.performAction("music", null, "pause", 0, true);
            PlayPauseButton.Content = "";
        }

        private void RewindClick(object sender, RoutedEventArgs e)
        {
            SmartThingsAPI_Actions.performAction("music", null, "previousTrack", 0, true);
        }

        private void FastforwardClick(object sender, RoutedEventArgs e)
        {
            SmartThingsAPI_Actions.performAction("music", null, "nextTrack", 0, true);
        }
        #endregion
    }
}
