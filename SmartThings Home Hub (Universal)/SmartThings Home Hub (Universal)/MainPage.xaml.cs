using System;
using Windows.UI.Xaml.Navigation;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler<object>(dispatchTimer_Tick);
            timer.Start();

            Image_Replace();
            Location_Replace();
        }

        void  dispatchTimer_Tick(object sender, object e)
        {
            // Placing time & date on lock
            Status_Time.Text = DateTime.Now.ToString("h" + ":" + "mm");
            Status_Date.Text = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
        }

        public void allLights_Click(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/6f9372eb-2568-4544-9fae-b530d9140166/command?type=helloHome&device=helloHome&command=Auto+Sleep&access_token=f2adcb57-b59c-4338-9b78-a541a400ec79s");
            HttpClient client = new HttpClient();
            client.SendAsync(request);

        }

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

        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

    }
}
