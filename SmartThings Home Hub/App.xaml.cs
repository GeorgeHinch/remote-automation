using NativeWifi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace SmartThings_Home_Hub
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Allows tracking page views, exceptions and other telemetry through the Microsoft Application Insights service.
        /// </summary>
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }

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
            LockTime.Text = DateTime.Now.ToString("h" + " " + "mm");
            LockDate.Text = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");

            // Getting network SSID & placing it on lock screen
            NetworkStatusResult currentStatus = networkStatuserrrrr.refresh();
            LockSSID.Text = currentStatus.Ssid;

            // Getting the current system power status.
            string strPowerLineStatus;
            strPowerLineStatus = string.Empty;
            switch (SystemParameters.PowerLineStatus)
            {
                case PowerLineStatus.Offline:
                    strPowerLineStatus = "Device is running on battery";
                    BitmapImage batAlert = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_battery_alert_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batAlert as ImageSource;
                    break;
                case PowerLineStatus.Online:
                    strPowerLineStatus = "Device is plugged in";
                    BitmapImage batConnected = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_power_connected_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batConnected as ImageSource;
                    break;
                case PowerLineStatus.Unknown:
                    strPowerLineStatus = "Power Status: Unknown";
                    BitmapImage batUnkown = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/resources/symbols/ic_battery_unknown_50px.png", UriKind.Absolute));
                    pwrStateIcon.Source = batUnkown as ImageSource;
                    break;
            }

            //Status icons popups
            pwrPopupLabel.Content = strPowerLineStatus;
            if (currentStatus.SigStrength.Equals(NetworkResultSignalStrength.ETHER))
            {
                sigPopupLabel.Content = "Connected via Ethernet";
            }
            else
            {
                sigPopupLabel.Content = currentStatus.Ssid;
            }


            // Getting the current WiFi signal strength
            switch (currentStatus.SigStrength)
            {
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

        async void btList()
        {
            btListbox.Items.Clear();

            var devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

            foreach (var device in devices)
            {
                btListbox.Items.Add(device);
            }
        }


        private void Show_pwrPopup_Click(object sender, RoutedEventArgs e)
        {
            pwrPopup.IsOpen = true;
        }

        private void Show_sigPopup_Click(object sender, RoutedEventArgs e)
        {
            sigPopup.IsOpen = true;
        }

        private void Show_btPopup_Click(object sender, RoutedEventArgs e)
        {
            btPopup.IsOpen = true;
        }

        private void Show_notifyPopup_Click(object sender, RoutedEventArgs e)
        {
            notifyPopup.IsOpen = true;
        }

        public static PowerLineStatus PowerLineStatus
        {
            get;
        }

        public object DeviceInformation
        {
            get; private set;
        }
    }
}
