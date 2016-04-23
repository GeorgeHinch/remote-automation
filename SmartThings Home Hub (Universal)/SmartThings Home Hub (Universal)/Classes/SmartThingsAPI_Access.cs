using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class SmartThingsAPI_Access
    {
        #region Gets the SmartThings app ID
        public static string getApp()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string app = "";

            /* Load SmartThings App ID */
            if (roamingSettings.Values["stAppID"] == null)
            {
                MainPage.mainPage.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                app = roamingSettings.Values["stAppID"].ToString();
            }

            return app;
        }
        #endregion

        #region Gets the SmartThings app token
        public static string getToken()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string token = "";

            /* Load SmartThings Access Token */
            if (roamingSettings.Values["stToken"] == null)
            {
                MainPage.mainPage.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                token = roamingSettings.Values["stToken"].ToString();
            }

            return token;
        }
        #endregion
    }
}
