﻿using SmartThings_Home_Hub__Universal_.Classes;
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

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// This page displays all of the SWITCH TYPE devices in the SmartThings Hub and allows you to toggle the status.
    /// </summary>
    public sealed partial class LightsPage : Page
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

        public LightsPage()
        {
            this.InitializeComponent();
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("switch");

            #region Checks to see if there are any lights
            if (devices.Count != 0)
            {
                loadDevices(devices);

                timer.Interval = TimeSpan.FromSeconds(5);
                EventHandler<Object> handlr = new EventHandler<object>(this.refreshTick);
                timer.Tick += handlr;
                timer.Start();
            }
            else
            {
                NoLights_TB.Visibility = Visibility.Visible;
            }
            #endregion
        }

        #region Loads and creates buttons on lights page
        public void loadDevices(List<SmartThingsHub> devices)
        {
            List<Button> deviceButtonList = new List<Button>();
            List<StackPanel> deviceStackpanelList = new List<StackPanel>();
            List<StackPanel> mainStackpanelList = new List<StackPanel>();
            StackPanel indexSP = new StackPanel();
            StackPanel mainIndexSP = new StackPanel();
            int indexVal = 1;
            int mainIndexVal = 1;

            #region Creates buttons from ST JSON
            foreach (SmartThingsHub sth in devices)
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
                }
                else { tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204)); }
                tbIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                tbIcon.TextAlignment = TextAlignment.Center;
                tbIcon.FontSize = 48;
                tbIcon.Margin = new Thickness(0, 0, 0, 15);
                tbIcon.Name = sth.device;
                Debug.WriteLine("Icon Name: " + tbIcon.Name + " |");
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
            #endregion

            #region Creates stackpanel rows of 4 buttons
            foreach (Button btn in deviceButtonList)
            {
                indexSP.Children.Add(btn);

                if (indexVal % 4 == 0)
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

            #region Creates stackpanels of two stackpanel rows
            foreach (StackPanel sp in deviceStackpanelList)
            {
                mainIndexSP.Children.Add(sp);

                if (mainIndexVal % 2 == 0)
                {
                    mainIndexSP.HorizontalAlignment = HorizontalAlignment.Center;
                    mainIndexSP.Height = 300;
                    mainIndexSP.VerticalAlignment = VerticalAlignment.Center;
                    mainIndexSP.Width = 700;
                    mainStackpanelList.Add(mainIndexSP);
                    mainIndexSP = new StackPanel();
                }

                mainIndexVal++;
            }

            if (mainIndexSP.Children.Count != 0)
            {
                mainStackpanelList.Add(mainIndexSP);
            }
            #endregion

            #region Adds main stackpanels to flipview
            foreach (StackPanel sp in mainStackpanelList)
            {
                light_flipView.Items.Add(sp);
            }
            #endregion
        }
        #endregion

        #region Button tapped handler to toggle lights
        public void toggleLight(object sender, RoutedEventArgs e)
        {
            Button sendr = (Button)sender;
            string device = sendr.Tag.ToString();
            string command = lightStatus(device);

            SmartThingsAPI_Actions.performAction("switch", device, command, 0, false);
        }
        #endregion

        #region Checks the on/off status of light
        public string lightStatus(string sender)
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

            if (deviceVal == "off")
            {
                command = "on";
            }
            else { command = "off"; }

            return command;
        }
        #endregion

        #region Refreshes lights every 5 seconds
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
                            if (sth.tile == "device" && sth.type == "switch")
                            {
                                TextBlock tb = (TextBlock)this.FindName(sth.device);

                                if (sth.value == "on")
                                {
                                    tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                                }
                                else if (sth.value == "off")
                                {
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
