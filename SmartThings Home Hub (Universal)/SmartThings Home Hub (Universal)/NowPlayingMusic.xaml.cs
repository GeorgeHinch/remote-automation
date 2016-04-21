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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NowPlayingMusic : Page
    {
        public NowPlayingMusic()
        {
            this.InitializeComponent();

            string app = getApp();
            string token = getToken();
            loadStatus(app, token);


            // Volume
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=level&value=5&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060404

            // Pause
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=pause&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060397

            // Play
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=play&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060398

            // Next Track
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=nextTrack&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461123759343

            // Previous Track
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=previousTrack&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461123759345
        }

        public void loadStatus(string app, string token)
        {
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

                                if(trackDescription.Before(" - ") != songTitle.Text)
                                {
                                    string song = trackDescription.Before(" - ");
                                    string artist = trackDescription.After(" - ");
                                    Debug.WriteLine("Song: " + song + " |");
                                    Debug.WriteLine("Artist: " + artist + " |");

                                    if (song.Trim() != "" && artist.Trim() != "")
                                    {
                                        songTitle.Text = song;
                                        songArtist.Text = artist;

                                        string albumArt = fetchArtwork(song, artist);

                                        if(albumArt != null)
                                        {
                                            songArt.Source = new BitmapImage(new Uri(albumArt, UriKind.Absolute));
                                        }
                                    }
                                    else { songTitle.Text = trackDescription; }
                                }

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

        public bool isPlaying = false;
        public bool isShuffle = false;
        public bool isRepeat = false;

        private void PlayPauseClick(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                //music.pause();
                performAction("pause");
                isPlaying = false;
                PlayPauseButton.Content = "";
                return;
            }

            //music.play();
            performAction("play");
            isPlaying = true;
            PlayPauseButton.Content = "";
            return;
        }

        private void RewindClick(object sender, RoutedEventArgs e)
        {
            performAction("previousTrack");
        }

        private void FastforwardClick(object sender, RoutedEventArgs e)
        {
            performAction("nextTrack");
        }

        private void ShuffleClick(object sender, RoutedEventArgs e)
        {
            if (isShuffle)
            {
                //music.shuffle();
                isShuffle = false;
                ShuffleButton.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 204));
                return;
            }

            //music.shuffle();
            isShuffle = true;
            ShuffleButton.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 66, 07));
            return;
        }

        private void RepeatClick(object sender, RoutedEventArgs e)
        {
            if (isRepeat)
            {
                //music.repeat();
                isRepeat = false;
                RepeatButton.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 204));
                return;
            }

            //music.repeat();
            isRepeat = true;
            RepeatButton.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 66, 97));
            return;
        }

        private void performAction(string command)
        {
            string app = getApp();
            string token = getToken();
            List<string> devices = getDevice(app, token);

            if (command == "play" || command == "pause")
            {
                foreach (string device in devices)
                {
                    string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&access_token=" + token;
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Get,
                        rqstMsg);
                    HttpClient client = new HttpClient();
                    client.SendAsync(request);
                }
            }
            else
            {
                string device = devices[0];
                string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&access_token=" + token;
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
                HttpClient client = new HttpClient();
                client.SendAsync(request);
            }
        }

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

        #region Gets all Sonos music devices in SmartThings
        public List<string> getDevice(string app, string token)
        {
            List<string> deviceIDList = new List<string>();

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

                        #region Adds device IDs to list
                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device" && sth.type == "music")
                            {
                                deviceIDList.Add(sth.device);
                            }
                        }
                        #endregion
                    }
                }
                return deviceIDList;
            }
            else { return null; }
        }
        #endregion

        #region Fetches artwork from lastFM
        public string fetchArtwork(string song, string artist)
        {
            string k = "73bd84173ee30b7e3d5e55c1c73f672a";
            string imgBMP = null;

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            

            string rqstMsg = "http://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key=" + k + "&artist=" + Uri.EscapeDataString(artist) + "&track=" + Uri.EscapeDataString(song) + "&format=json";
            Debug.WriteLine("Song URL: " + rqstMsg + " |");

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
                        try
                        {
                            var serializer = new DataContractJsonSerializer(typeof(LastFM_SongFind));
                            LastFM_SongFind foundSong = (LastFM_SongFind)serializer.ReadObject(stream);
                            IList<SongImage> image = (IList<SongImage>)foundSong.track.album.image;

                            #region Gets and sets album info from JSON
                            foreach (SongImage img in image)
                            {
                                if (img.size == "extralarge")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "large")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "medium")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "small")
                                {
                                    imgBMP = img.text;
                                }
                                else
                                {
                                    imgBMP = null;
                                }
                            }
                            #endregion
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine("Album Artwork Exception: " + ex.Message + " |");
                            return null;
                        }
                    }
                }
            }
            return imgBMP;
        }
        #endregion

        private void PlayPauseButton_Checked(object sender, RoutedEventArgs e)
        {
            performAction("play");
            PlayPauseButton.Content = "";
        }

        private void PlayPauseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            performAction("pause");
            PlayPauseButton.Content = "";
        }
    }
}
