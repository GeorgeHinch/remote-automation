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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void OnAccountsButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(AccountSettings));
        }

        private void OnBluetoothButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(BluetoothSettings));
        }

        private void OnLEDsButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(LEDsSettings));
        }

        private void OnLocationButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(LocationSettings));
        }

        private void OnNetworkButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(NetworkSettings));
        }

        private void OnPersonalizationButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(PersonalizationSettings));
        }

        private void OnPowerButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(PowerSettings));
        }

        private void OnUpdatesButtonChecked(object sender, RoutedEventArgs e)
        {
            settingsFrame.Navigate(typeof(UpdatesSettings));
        }
    }
}
