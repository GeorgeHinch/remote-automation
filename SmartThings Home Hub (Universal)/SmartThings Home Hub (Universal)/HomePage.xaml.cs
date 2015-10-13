using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.Devices.Input;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };

        public HomePage()
        {
            this.InitializeComponent();
            this.timerReset();
        }

        private void timerReset()
        {
            _timer.Start();
            Debug.WriteLine("shit");
        }

        private void HomePage_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // restart the timer whenever the user moves the cursor
            timerReset();
        }

        private void Timer_Tick(object sender, object e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void btnPlay_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _timer.Tick += Timer_Tick;
            this.PointerMoved += HomePage_PointerMoved;

            _timer.Start();
        }

        private void btnPause_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _timer.Tick -= Timer_Tick;
            this.PointerMoved -= HomePage_PointerMoved;

            _timer.Stop();
        }










        /* public void AttachEvents()
        {
            Application.Current.RootVisual.MouseMove += new MouseEventHandler(RootVisual_MouseMove);
            Application.Current.RootVisual.KeyDown += new KeyEventHandler(RootVisual_KeyDown);

            MouseDevice.MouseMoved; 

            Application.Current.RootVisual.AddHandler(UIElement.MouseLeftButtonDownEvent, (MouseButtonEventHandler)RootVisual_MouseButtonDown, true);
            Application.Current.RootVisual.AddHandler(UIElement.MouseRightButtonDownEvent, (MouseButtonEventHandler)RootVisual_MouseButtonDown, true);
        }

        public event TypedEventHandler<MouseDevice, MouseEventArgs> MouseMoved;
        */





        private void Lights_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LightsPage));
        }

        private void Locks_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LocksPage));
        }

        private void Weather_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WeatherPage));
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MusicPage));
        }

        private void Security_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SecurityPage));
        }

        private void Presence_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PresencePage));
        }

        private void Motion_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MotionPage));
        }

        private void Enviroment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EnviromentPage));
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewsPage));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }
    }
}
