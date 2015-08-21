﻿using System;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler<object>(dispatchTimer_Tick);
            timer.Start();
        }

        void  dispatchTimer_Tick(object sender, object e)
        {
            // Placing time & date on lock
            Status_Time.Text = DateTime.Now.ToString("h" + ":" + "mm");
            Status_Date.Text = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
        }

        private void Status_Time_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
