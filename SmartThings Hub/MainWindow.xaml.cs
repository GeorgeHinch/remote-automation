using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.UI.Xaml.Media.Imaging;

namespace SmartThings_Hub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NetworkStatus networkStatuserrrrr;
        //ResourceDictionary dic;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            networkStatuserrrrr = new NetworkStatus();
            //dic = this.Resources.MergedDictionaries[0];
        }

        void timer_Tick(object sender, EventArgs e)
        {
            LockTime.Content = DateTime.Now.ToString("h" + " " + "mm");
            LockDate.Content = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
            LockPower.Content = SystemParameters.PowerLineStatus;
            NetworkStatusResult currentStatus = networkStatuserrrrr.refresh();
            LockSSID.Content = currentStatus.Ssid;
            // Getting the current system power status.
            string strPowerLineStatus;
            strPowerLineStatus = string.Empty;
            switch (SystemParameters.PowerLineStatus)
            {
                case PowerLineStatus.Offline:
                    strPowerLineStatus = "PowerLineStatus: Offline";
                    pwrStateIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_battery_alert_50px.png”));
                    break;
                case PowerLineStatus.Online:
                    strPowerLineStatus = "PowerLineStatus: Online";
                    pwrStateIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_power_connected_50px.png”));
                    break;
                case PowerLineStatus.Unknown:
                    strPowerLineStatus = "PowerLineStatus: Unknown";
                    pwrStateIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_battery_unknown_50px.png”));
                    break;
            }
            Console.WriteLine(strPowerLineStatus);

            // Getting the current WiFi signal strength
            switch (currentStatus.SigStrength){
                case NetworkResultSignalStrength.ONE:
                    LockSigStrength.Content = "ONE";
                    sigStrengthIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_signal_wifi_statusbar_1_bar_50px.png”));
                    //sigStrengthIcon.Source = ((Image)dic["ic_signal_wifi_statusbar_4_bar_50px"]).Source;
                    break;
                case NetworkResultSignalStrength.TWO:
                    LockSigStrength.Content = "TWO";
                    sigStrengthIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_signal_wifi_statusbar_2_bar_50px.png”));
                    break;
                case NetworkResultSignalStrength.THREE:
                    LockSigStrength.Content = "THREE";
                    sigStrengthIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_signal_wifi_statusbar_3_bar_50px.png”));
                    break;
                case NetworkResultSignalStrength.FOUR:
                    LockSigStrength.Content = "FOUR";
                    sigStrengthIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/is_signal_wifi_statusbar_4_bar_50px.png”));
                    break;
                case NetworkResultSignalStrength.ZERO:
                    LockSigStrength.Content = "ZERO";
                    sigStrengthIcon.Source = new BitmapImage(new Uri(base.BaseUri, “/resources/symbols/ic_signal_wifi_statusbar_connected_no_internet_50px.png”));
                    break;
            }
        }

        public static PowerLineStatus PowerLineStatus
        {
            get;
        }
    }
}






/// http://www.wpf-tutorial.com/misc/dispatchertimer/
/// http://www.thecodingguys.net/tutorials/csharp/csharp-dates-and-times
