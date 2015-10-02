using System;
using Windows.UI.Xaml.Navigation;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;

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

        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

    }
}
