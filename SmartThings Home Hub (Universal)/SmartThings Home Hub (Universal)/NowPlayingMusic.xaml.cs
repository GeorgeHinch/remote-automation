using SmartThings_Home_Hub__Universal_.Classes;
using SonosSharp;
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
using System.Threading;
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
        #region Stops timers on navigation from page
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();

            base.OnNavigatedFrom(e);
        }
        #endregion

        public DispatcherTimer timer = new DispatcherTimer();

        public NowPlayingMusic()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(5);
            EventHandler<Object> handlr = new EventHandler<object>(this.loadStatus);
            timer.Tick += handlr;
            timer.Start();
        }

        public bool isShuffle = false;
        public bool isRepeat = false;

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

        #region Loads and sets information from SmartThings JSON for now playing song
        public void loadStatus(object sender, object e)
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
                                    Debug.WriteLine("Song: " + song + " |");
                                    Debug.WriteLine("Artist: " + artist + " |");

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
        #endregion

        #region Seeing what's up with SonosSharp
        public void sonosSharp()
        {
            //TrackMetaData md = SonosSharp.TrackMetaData
        }
        #endregion
    }
}
