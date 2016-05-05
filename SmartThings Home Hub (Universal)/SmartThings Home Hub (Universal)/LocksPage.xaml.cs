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
    public sealed partial class LocksPage : Page
    {
        #region Stops timers on navigation from page
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }

            base.OnNavigatedFrom(e);
        }
        #endregion

        public DispatcherTimer timer = new DispatcherTimer();

        public LocksPage()
        {
            this.InitializeComponent();
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("lock");

            #region Checks to see if there are any locks
            if (devices.Count != 0)
            {
                loadDevices();

                timer.Interval = TimeSpan.FromSeconds(5);
                EventHandler<Object> handlr = new EventHandler<object>(this.refreshTick);
                timer.Tick += handlr;
                timer.Start();
            }
            else
            {
                NoLocks_TB.Visibility = Visibility.Visible;
            }
            #endregion
        }

        #region Loads and creates buttons on locks page
        public void loadDevices()
        {
            List<Button> deviceButtonList = new List<Button>();
            List<StackPanel> deviceStackpanelList = new List<StackPanel>();
            StackPanel indexSP = new StackPanel();
            int indexVal = 1;

            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("lock");

            #region Creates buttons from ST JSON
            foreach (SmartThingsHub sth in devices)
            {
                Button btn = new Button();
                StackPanel sp = new StackPanel();
                TextBlock tbIcon = new TextBlock();
                TextBlock tbName = new TextBlock();
                TextBlock tbType = new TextBlock();


                #region Create icon textblock
                if (sth.value == "locked")
                {
                    tbIcon.Text = WebUtility.HtmlDecode("&#xE1F6;");
                    tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                }
                else
                {
                    tbIcon.Text = WebUtility.HtmlDecode("&#xE1F7;");
                    tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                }
                tbIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                tbIcon.TextAlignment = TextAlignment.Center;
                tbIcon.FontSize = 48;
                tbIcon.Margin = new Thickness(0, 0, 0, 15);
                tbIcon.Name = sth.device;
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
                btn.Width = 175;
                btn.Click += new RoutedEventHandler(toggleLock);
                btn.Background = null;
                btn.BorderBrush = null;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Tag = sth.device;
                #endregion

                #region Add button to list
                deviceButtonList.Add(btn);
                #endregion
            }
            #endregion

            #region Creates stackpanel rows of 4 buttons
            foreach (Button btn in deviceButtonList)
            {
                indexSP.Children.Add(btn);

                if (indexVal % 3 == 0)
                {
                    indexSP.Orientation = Orientation.Horizontal;
                    indexSP.Margin = new Thickness(0, 20, 0, 0);
                    deviceStackpanelList.Add(indexSP);
                    indexSP = new StackPanel();
                }

                indexVal++;
            }

            if (indexSP.Children.Count != 0)
            {
                indexSP.Orientation = Orientation.Horizontal;
                indexSP.Margin = new Thickness(0, 20, 0, 0);
                deviceStackpanelList.Add(indexSP);
            }
            #endregion

            #region Adds main stackpanels to flipview
            foreach (StackPanel sp in deviceStackpanelList)
            {
                sp.VerticalAlignment = VerticalAlignment.Center;
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                lock_flipView.Items.Add(sp);
            }
            #endregion
        }
        #endregion

        #region Button tapped handler to toggle locks
        public void toggleLock(object sender, RoutedEventArgs e)
        {
            Button sendr = (Button)sender;
            string device = sendr.Tag.ToString();
            string command = lockStatus(device);

            SmartThingsAPI_Actions.performAction("lock", device, command, 0, false);
        }
        #endregion

        #region Checks the lock/unlock status of lock
        public string lockStatus(string sender)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();
            string command;
            string deviceVal = "";

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

            if (deviceVal == "locked")
            {
                command = "unlock";
            }
            else { command = "lock"; }

            return command;
        }
        #endregion

        #region Refreshes locks every 5 seconds
        async void refreshTick(object sender, object e)
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

                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device" && sth.type == "lock")
                            {
                                TextBlock tb = (TextBlock)this.FindName(sth.device);

                                if (sth.value == "locked")
                                {
                                    tb.Text = WebUtility.HtmlDecode("&#xE1F6;");
                                    tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                                }
                                else 
                                {
                                    tb.Text = WebUtility.HtmlDecode("&#xE1F7;");
                                    tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
