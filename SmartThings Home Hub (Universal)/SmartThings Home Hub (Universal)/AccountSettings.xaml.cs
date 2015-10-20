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
    public sealed partial class AccountSettings : Page
    {
        public AccountSettings()
        {
            this.InitializeComponent();

            st_AppId_Load();
            st_Token_Load();
        }

        private void st_AppId_Save(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings App ID */
            roamingSettings.Values["stAppID"] = AppIDBox.Text.ToString();
        }

        private void st_Token_Save(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings Access Token */
            roamingSettings.Values["stToken"] = AccessTokenBox.Text.ToString();
        }

        private void st_AppId_Load()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load SmartThings App ID state */
            if (roamingSettings.Values["stAppID"] == null)
            {
                AppIDBox.PlaceholderText = "App ID";
            }
            else
            {
                var stAppID = roamingSettings.Values["stAppID"].ToString();
                AppIDBox.PlaceholderText = stAppID;
            }
        }

        private void st_Token_Load()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load SmartThings Access Token state */
            if (roamingSettings.Values["stToken"] == null)
            {
                AccessTokenBox.PlaceholderText = "Access Token";
            }
            else
            {
                var stToken = roamingSettings.Values["stToken"].ToString();
                AccessTokenBox.PlaceholderText = stToken;
            }
        }
    }
}
