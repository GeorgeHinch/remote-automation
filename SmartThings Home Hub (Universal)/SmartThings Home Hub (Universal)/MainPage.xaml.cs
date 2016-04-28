using System;
using Windows.UI.Xaml.Navigation;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using SmartThings_Home_Hub__Universal_.Classes;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        #region Stops timers on navigation from page
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timerClock.Stop();
            timerStatus.Stop();

            base.OnNavigatedFrom(e);
        }
        #endregion

        public static MainPage mainPage;

        public DispatcherTimer timerClock = new DispatcherTimer();
        public DispatcherTimer timerStatus = new DispatcherTimer();

        public MainPage()
        {
            InitializeComponent();
            statusTimer_Tick(this, null);
            
            timerClock.Interval = TimeSpan.FromSeconds(1);
            timerClock.Tick += new EventHandler<object>(dispatchTimer_Tick);
            timerClock.Start();

            timerStatus.Interval = TimeSpan.FromSeconds(30);
            timerStatus.Tick += new EventHandler<object>(statusTimer_Tick);
            timerStatus.Start();

            Image_Replace();
            Location_Replace();
        }
        
        #region Timers to update lock screen
        void dispatchTimer_Tick(object sender, object e)
        {
            // Placing time & date on lock
            Status_Time.Text = DateTime.Now.ToString("h" + ":" + "mm");
            Status_Date.Text = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
        }

        void statusTimer_Tick(object sender, object e)
        {
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("mode");
            Status_mode.Text = devices[0].mode;
        }
        #endregion

        #region Actions for buttons on lock screen
        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void HouseStatus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("mode");
            SmartThingsHub hub = devices[0];
            IList<string> modes = hub.modes;

            foreach (string mode in modes)
            {
                Button btn = new Button();
                btn.Background = new SolidColorBrush(Color.FromArgb(255, 75, 75, 75));
                btn.Foreground = new SolidColorBrush(Colors.White);
                btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Height = 50;
                btn.Content = mode;
                btn.Margin = new Thickness(0, 5, 0, 0);
                btn.FontWeight = FontWeights.Thin;
                btn.Tag = mode;
                btn.Tapped += new TappedEventHandler(ModeSelect_Tapped);

                ModeSelect_Stackpanel.Children.Add(btn);
            }

            ModeSelect_Grid.Visibility = Visibility.Visible;
        }

        private void ModeSelect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button sdr = (Button)sender;
            SmartThingsAPI_Actions.performAction("mode", null, sdr.Tag.ToString(), 0, false);

            ModeSelect_Grid.Visibility = Visibility.Collapsed;

            ModeSelect_Stackpanel.Children.Clear();

            statusTimer_Tick(this, null);
        }

        public void allLights_Click(object sender, RoutedEventArgs e)
        {
            SmartThingsAPI_Actions.performAction("switch", null, "off", 0, true);
        }

        public void custom1_Click(object sender, RoutedEventArgs e)
        {
            SmartThingsAPI_Actions.performAction("switch", "ba69bfdf-ae9a-44ac-ac38-c16cc4cadf1b", "on", 0, false);
        }

        public void custom2_Click(object sender, RoutedEventArgs e)
        {
            SmartThingsAPI_Actions.performAction("switch", "ba69bfdf-ae9a-44ac-ac38-c16cc4cadf1b", "on", 0, false);
        }
        #endregion

        #region Customizes the lock screen background. Can be changed using the image picker on the Personalization page.
        public void Image_Replace()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            Debug.WriteLine(roamingSettings.Values["LockBackgroundImage"]);

            if (roamingSettings.Values["LockBackgroundImage"] == null)
            {
                roamingSettings.Values["LockBackgroundImage"] = "LockBackgroundImage_1";
                LockBackgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/28H.jpg"));
            } else
            {
                if ((string)roamingSettings.Values["LockBackgroundImage"] == "LockBackgroundImage_1")
                {
                    LockBackgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/28H.jpg"));
                }
                else if ((string)roamingSettings.Values["LockBackgroundImage"] == "LockBackgroundImage_2")
                {
                    LockBackgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/Lock_Background_2.jpg"));
                }
                else if ((string)roamingSettings.Values["LockBackgroundImage"] == "LockBackgroundImage_3")
                {
                    LockBackgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/Lock_Background_3.jpg"));
                } else
                {
                    LockBackgroundImage.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/28H.jpg"));
                }
            }
        }
        #endregion
        
        #region Customizes the location text on the lock screen. Options can be sellected with a toggle in the Personalization page.
        public void Location_Replace()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            Debug.WriteLine(roamingSettings.Values["locRadio"]);

            if (roamingSettings.Values["locRadio"] == null)
            {
                HouseStatusLabel.Text = "home is set to...";
            }
            else
            {
                if ((string)roamingSettings.Values["locRadio"] == "LocHome")
                {
                    HouseStatusLabel.Text = "home is set to...";
                }
                else if ((string)roamingSettings.Values["locRadio"] == "LocApartment")
                {
                    HouseStatusLabel.Text = "apartment is set to...";
                }
                else if ((string)roamingSettings.Values["locRadio"] == "LocOffice")
                {
                    HouseStatusLabel.Text = "office is set to...";
                }
                else if ((string)roamingSettings.Values["locRadio"] == "LocRoom")
                {
                    HouseStatusLabel.Text = "room is set to...";
                }
                else
                {
                    HouseStatusLabel.Text = "home is set to...";
                }
            }
        }
        #endregion
    }
}
