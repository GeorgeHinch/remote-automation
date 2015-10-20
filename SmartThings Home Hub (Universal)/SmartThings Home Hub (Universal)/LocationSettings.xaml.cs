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
    public sealed partial class LocationSettings : Page
    {
        public LocationSettings()
        {
            this.InitializeComponent();

            ZIP_Load();
        }

        private void ZIP_Save(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store ZIP Code */
            roamingSettings.Values["ZipCode"] = ZipCodeBox.Text.ToString();
        }

        private void key_Click(object sender, RoutedEventArgs e)
        {
            if (ZipCodeBox.Text.Length <= 4)
            {
                Button btn = (Button)e.OriginalSource;
                string s = btn.Content.ToString();
                ZipCodeBox.Text += s;
            }
        }

        private void ZIP_Load()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load Zip Code state */
            if (roamingSettings.Values["ZipCode"] == null)
            {
                ZipCodeBox.PlaceholderText = "Zip Code";
            }
            else
            {
                var zipCode = roamingSettings.Values["ZipCode"].ToString();
                ZipCodeBox.PlaceholderText = zipCode;
            }
        }
    }
}
