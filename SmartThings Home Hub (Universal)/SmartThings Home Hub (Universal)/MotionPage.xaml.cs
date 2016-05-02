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
    public sealed partial class MotionPage : Page
    {
        #region Stops timers on navigation from page
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();

            base.OnNavigatedFrom(e);
        }
        #endregion

        public DispatcherTimer timer = new DispatcherTimer();

        public MotionPage()
        {
            this.InitializeComponent();
            
            timer.Interval = TimeSpan.FromSeconds(5);
            EventHandler<Object> stupd = new EventHandler<object>(this.refreshTick);
            timer.Tick += stupd;
            timer.Start();

            loadDevices();
        }

        #region Loads and creates icons for motion devices
        public void loadDevices()
        {
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("motion");

            foreach(SmartThingsHub sth in devices)
            {
                StackPanel sp = new StackPanel();
                TextBlock tbIcon = new TextBlock();
                TextBlock tbName = new TextBlock();
                TextBlock tbType = new TextBlock();


                #region Create icon textblock
                tbIcon.Name = sth.device;
                tbIcon.Text = WebUtility.HtmlDecode("&#59389;");
                if (sth.value == "active" || sth.value == "open")
                {
                    tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                }
                else { tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204)); }
                tbIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                tbIcon.TextAlignment = TextAlignment.Center;
                tbIcon.FontSize = 96;
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

                #region Add stackpanel to list
                sp.Orientation = Orientation.Vertical;
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.VerticalAlignment = VerticalAlignment.Center;
                sp.Width = 250;
                motion_Stackpanel.Children.Add(sp);
                #endregion
            }
        }
        #endregion

        #region Refreshes motion and contact every 5 seconds
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
                            if (sth.tile == "device" && (sth.type == "motion" || sth.type == "contact"))
                            {
                                TextBlock tb = (TextBlock)this.FindName(sth.device);

                                if (sth.value == "active" || sth.value == "open")
                                {
                                    tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                                }
                                else if (sth.value == "inactive" || sth.value == "closed")
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
