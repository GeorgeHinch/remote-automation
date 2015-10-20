using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class PowerSettings : Page
    {
        public PowerSettings()
        {
            this.InitializeComponent();
        }

        private GpioController gpio;

        public void ShutdownComputer(object target, RoutedEventArgs args)
        {
            Debug.WriteLine("Shutdown");

            this.gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                Debug.WriteLine("No ShutdownManager available.");
            }
            else
            {
                ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, new TimeSpan(0));
            }
        }

        public void RebootComputer(object target, RoutedEventArgs args)
        {
            Debug.WriteLine("Reboot");

            this.gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                Debug.WriteLine("No ShutdownManager available.");
            }
            else
            {
                ShutdownManager.BeginShutdown(ShutdownKind.Restart, new TimeSpan(0));
            }
        }

        public void LockComputer(object target, RoutedEventArgs arg)
        {
            Debug.WriteLine("Lock");

            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(MainPage));

        }
    }
}
