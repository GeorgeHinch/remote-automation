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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicPage : Page
    {
        public MusicPage()
        {
            this.InitializeComponent();
        }

        private void OnPlayingButtonChecked(object sender, RoutedEventArgs e)
        {
            musicFrame.Navigate(typeof(NowPlayingMusic));
        }

        private void OnQueueButtonChecked(object sender, RoutedEventArgs e)
        {
            musicFrame.Navigate(typeof(QueueMusic));
        }

        private void OnLibraryButtonChecked(object sender, RoutedEventArgs e)
        {
            musicFrame.Navigate(typeof(LibraryMusic));
        }

        private void OnRoomsButtonChecked(object sender, RoutedEventArgs e)
        {
            musicFrame.Navigate(typeof(RoomsMusic));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
