﻿using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// This page displays all of the CAMERA TYPE devices in the SmartThings Hub and captures photos.
    /// </summary>
    public sealed partial class CameraPage : Page
    {
        #region Stops timers on navigation from page
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        #endregion

        public CameraPage()
        {
            this.InitializeComponent();
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("camera");

            #region Checks to see if there are any cameras
            if (devices.Count != 0)
            {
                loadCameras(devices);
            }
            else
            {
                NoCamera_TB.Visibility = Visibility.Visible;
                cameras_mainView.Visibility = Visibility.Collapsed;
            }
            #endregion
        }

        public void loadCameras(List<SmartThingsHub> devices)
        {
            #region Creates textblocks for each camera TYPE
            foreach (SmartThingsHub device in devices)
            {
                TextBlock headTB = new TextBlock();
                headTB.Text = device.name;
                headTB.FontWeight = FontWeights.Medium;
                headTB.TextTrimming = TextTrimming.WordEllipsis;

                TextBlock descTB = new TextBlock();
                descTB.Text = device.type;
                descTB.TextTrimming = TextTrimming.WordEllipsis;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                sp.Margin = new Thickness(0, 0, 0, 10);
                sp.Children.Add(headTB);
                sp.Children.Add(descTB);
                
                RadioButton camera_rb = new RadioButton();
                camera_rb.Content = sp;
                camera_rb.Tag = device.device;
                camera_rb.Checked += new RoutedEventHandler(radioButton_Checked);

                this.cameraListStackpanel.Children.Add(camera_rb);
            }
            #endregion
        }

        #region Changes detail view based on camera selected
        private void radioButton_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            RadioButton radioButton = ((RadioButton)sender);
            string deviceID = (string)radioButton.Tag;

            foreach (RadioButton rb in cameraListStackpanel.Children)
            {
                if (radioButton.Content != rb.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            cameraViewImage.Source = new BitmapImage();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
        #endregion
    }
}
