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

            // Volume
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=level&value=5&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060404

            // Pause
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=pause&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060397

            // Play
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=play&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461119060398

            // Next Track
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=nextTrack&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461123759343

            // Previous Track
            // https://graph.api.smartthings.com/api/smartapps/installations/45b32e18-a124-4e1a-ba86-b92f6068c8f6/command?type=music&device=9e55e73e-2061-4be9-9fc1-e26d09b4d63b&command=previousTrack&access_token=82738eb3-f9c7-4f4c-a7f6-d55952ee7ea2&_=1461123759345
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
