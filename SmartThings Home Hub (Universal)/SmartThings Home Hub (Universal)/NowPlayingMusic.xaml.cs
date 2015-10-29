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
    public sealed partial class NowPlayingMusic : Page
    {
        public NowPlayingMusic()
        {
            this.InitializeComponent();
        }

        public bool isPlaying = false;
        public bool isShuffle = false;
        public bool isRepeat = false;

        private void PlayPauseClick(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                //music.pause();
                isPlaying = false;
                PlayPauseButton.Content = "";
                return;
            }

            //music.play();
            isPlaying = true;
            PlayPauseButton.Content = "";
            return;
        }

        private void RewindClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void FastforwardClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ShuffleClick(object sender, RoutedEventArgs e)
        {
            if (isShuffle)
            {
                //music.shuffle();
                isShuffle = false;
                ShuffleButton.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 204));
                return;
            }

            //music.shuffle();
            isShuffle = true;
            ShuffleButton.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 66, 07));
            return;
        }

        private void RepeatClick(object sender, RoutedEventArgs e)
        {
            if (isRepeat)
            {
                //music.repeat();
                isRepeat = false;
                RepeatButton.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 204));
                return;
            }

            //music.repeat();
            isRepeat = true;
            RepeatButton.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 66, 97));
            return;
        }
    }
}
