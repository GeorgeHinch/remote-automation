using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SmartThings_Hub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NetworkStatus networkStatuserrrrr;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            networkStatuserrrrr = new NetworkStatus();
            ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Placing time & date on lock
            LockTime.Content = DateTime.Now.ToString("h" + " " + "mm");
            LockDate.Content = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");

            // Getting network SSID & placing it on lock screen
            NetworkStatusResult currentStatus = networkStatuserrrrr.refresh();
            LockSSID.Content = currentStatus.Ssid;

            // Getting the current system power status.
            string strPowerLineStatus;
            strPowerLineStatus = string.Empty;
            switch (SystemParameters.PowerLineStatus)
            {
                case PowerLineStatus.Offline:
                    strPowerLineStatus = "PowerLineStatus: Offline";
                    BitmapImage batAlert = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_battery_alert_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batAlert as ImageSource;
                    break;
                case PowerLineStatus.Online:
                    strPowerLineStatus = "PowerLineStatus: Online";
                    BitmapImage batConnected = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_power_connected_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batConnected as ImageSource;
                    break;
                case PowerLineStatus.Unknown:
                    strPowerLineStatus = "PowerLineStatus: Unknown";
                    BitmapImage batUnkown = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_battery_unknown_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batUnkown as ImageSource;
                    break;
            }
            Console.WriteLine(strPowerLineStatus);

            // Getting the current WiFi signal strength
            switch (currentStatus.SigStrength){
                case NetworkResultSignalStrength.ONE:
                    BitmapImage sigOne = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_wifi_statusbar_1_bar_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigOne as ImageSource;
                    break;
                case NetworkResultSignalStrength.TWO:
                    BitmapImage sigTwo = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_wifi_statusbar_2_bar_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigTwo as ImageSource;
                    break;
                case NetworkResultSignalStrength.THREE:
                    BitmapImage sigThree = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_wifi_statusbar_3_bar_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigThree as ImageSource;
                    break;
                case NetworkResultSignalStrength.FOUR:
                    BitmapImage sigFour = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_wifi_statusbar_4_bar_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigFour as ImageSource;
                    break;
                case NetworkResultSignalStrength.ZERO:
                    BitmapImage sigZero = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_wifi_statusbar_connected_no_internet_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigZero as ImageSource;
                    break;
                case NetworkResultSignalStrength.ETHER:
                    BitmapImage sigEther = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_signal_ethernet_50px.png", UriKind.Absolute));
                    sigStrengthIcon.Source = sigEther as ImageSource;
                    break;
            }
        }

        public static PowerLineStatus PowerLineStatus
        {
            get;
        }
    }
}