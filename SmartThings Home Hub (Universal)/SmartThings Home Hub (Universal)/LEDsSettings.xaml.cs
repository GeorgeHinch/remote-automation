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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LEDsSettings : Page
    {
        public LEDsSettings()
        {
            this.InitializeComponent();
        }

        private void FadeLED_Checked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).getLEDs().fadeTogether();
        }

        private void BlueLED_Checked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).getLEDs().setColorAll(0, 0, 255);
        }

        private void WhiteLED_Checked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).getLEDs().setColorAll(255, 255, 255);
        }

        private void RainbowLED_Checked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).getLEDs().rainbowTogether();

        }
    }
}
