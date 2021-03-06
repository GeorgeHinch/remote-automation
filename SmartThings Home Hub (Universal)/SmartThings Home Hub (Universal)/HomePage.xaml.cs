﻿using System;
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
        public HomePage()
        {
            this.InitializeComponent();
        }

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

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CameraPage));
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
