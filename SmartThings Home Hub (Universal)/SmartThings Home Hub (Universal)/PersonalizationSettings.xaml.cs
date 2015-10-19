using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalizationSettings : Page
    {
        public PersonalizationSettings()
        {
            this.InitializeComponent();

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load LED power toggle state */
            if (roamingSettings.Values["alertStatus"] != null)
            {
                object alertValue = roamingSettings.Values["alertStatus"];
                AlertModalsSwitch.IsOn = (bool)alertValue;
            }
            else
            {
                Debug.WriteLine("You fucked up.");
                return;
            }

            alertOff_Toggler();
            indAlert_Toggler();
        }

        private void LockImage1_Click(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store background 1 */
            roamingSettings.Values["LockBackgroundImage"] = "LockBackgroundImage_1";

            /*new MainPage().Image_Replace();*/
        }

        private void LockImage2_Click(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store background 2 */
            roamingSettings.Values["LockBackgroundImage"] = "LockBackgroundImage_2";

            /*new MainPage().Image_Replace();*/
        }

        private void LockImage3_Click(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store background 3 */
            roamingSettings.Values["LockBackgroundImage"] = "LockBackgroundImage_3";

            /*new MainPage().Image_Replace();*/
        }

        public void alertStatus_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store alert on/off status */
            roamingSettings.Values["alertStatus"] = AlertModalsSwitch.IsOn;

            alertOff_Toggler();
        }

        public void alertOff_Toggler()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            bool alertValue = (bool)roamingSettings.Values["alertStatus"];


            if (alertValue == true)
            {
                alertsToggle_Enable();
            }
            else if (alertValue == false)
            {
                alertsToggle_Disable();
            }
        }

        private void alertsToggle_Enable()
        {
            PulseLEDSwitch.IsEnabled = true;
            SmartThingsLowBatterySwitch.IsEnabled = true;
            SmartThingsKnockSwitch.IsEnabled = true;
            SmartThingsEnviromentSwitch.IsEnabled = true;
            SmartThingsAwayDoorSwitch.IsEnabled = true;
            SeverWeatherSwitch.IsEnabled = true;
            PowerStatusSwitch.IsEnabled = true;
            NetworkStatusSwitch.IsEnabled = true;
        }

        private void alertsToggle_Disable()
        {
            PulseLEDSwitch.IsEnabled = false;
            SmartThingsLowBatterySwitch.IsEnabled = false;
            SmartThingsKnockSwitch.IsEnabled = false;
            SmartThingsEnviromentSwitch.IsEnabled = false;
            SmartThingsAwayDoorSwitch.IsEnabled = false;
            SeverWeatherSwitch.IsEnabled = false;
            PowerStatusSwitch.IsEnabled = false;
            NetworkStatusSwitch.IsEnabled = false;
        }
              

        private void PulseLEDSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store LED Pulse on/off status */
            roamingSettings.Values["PulseLEDSwitch"] = PulseLEDSwitch.IsOn;
        }

        private void SmartThingsLowBatterySwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings battery alert on/off status */
            roamingSettings.Values["SmartThingsLowBatterySwitch"] = SmartThingsLowBatterySwitch.IsOn;

            if (SmartThingsLowBatterySwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.SmartThingsLowBattery_Alert alert = new alerts.SmartThingsLowBattery_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.SmartThingsLowBattery_Alert alert = new alerts.SmartThingsLowBattery_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("SmartThingsLowBatterySwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void SmartThingsKnockSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings knock alert on/off status */
            roamingSettings.Values["SmartThingsKnockSwitch"] = SmartThingsKnockSwitch.IsOn;

            if (SmartThingsKnockSwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.SmartThingsKnock_Alert alert = new alerts.SmartThingsKnock_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.SmartThingsKnock_Alert alert = new alerts.SmartThingsKnock_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("SmartThingsKnockSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("SmartThingsKnockSwitch is off.");
            }
        }

        private void SmartThingsAwayDoorSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings away door alert on/off status */
            roamingSettings.Values["SmartThingsAwayDoorSwitch"] = SmartThingsAwayDoorSwitch.IsOn;

            if (SmartThingsAwayDoorSwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.SmartThingsAwayDoor_Alert alert = new alerts.SmartThingsAwayDoor_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.SmartThingsAwayDoor_Alert alert = new alerts.SmartThingsAwayDoor_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("SmartThingsAwayDoorSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void SmartThingsEnviromentSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store SmartThings enviroment alert on/off status */
            roamingSettings.Values["SmartThingsEnviromentSwitch"] = SmartThingsEnviromentSwitch.IsOn;

            if (SmartThingsEnviromentSwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.SmartThingsEnviroment_Alert alert = new alerts.SmartThingsEnviroment_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.SmartThingsEnviroment_Alert alert = new alerts.SmartThingsEnviroment_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("SmartThingsEnviromentSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void SeverWeatherSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store severe weather alert on/off status */
            roamingSettings.Values["SeverWeatherSwitch"] = SeverWeatherSwitch.IsOn;

            if (SeverWeatherSwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.SevereWeatherSwitch_Alert alert = new alerts.SevereWeatherSwitch_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.SevereWeatherSwitch_Alert alert = new alerts.SevereWeatherSwitch_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("SevereWeatherSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void PowerStatusSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store power status alert on/off status */
            roamingSettings.Values["PowerStatusSwitch"] = PowerStatusSwitch.IsOn;

            if (PowerStatusSwitch.IsOn == true)
            {
                if ((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.PowerStatusSwitch_Alert alert = new alerts.PowerStatusSwitch_Alert();
                    alert.alert();
                }
                else if ((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.PowerStatusSwitch_Alert alert = new alerts.PowerStatusSwitch_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("PowerSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void NetworkStatusSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store network status alert on/off status */
            roamingSettings.Values["NetworkStatusSwitch"] = NetworkStatusSwitch.IsOn;
        
            if(NetworkStatusSwitch.IsOn == true)
            {
                if((bool)roamingSettings.Values["PulseLEDSwitch"] == false)
                {
                    alerts.NetworkStatusSwitch_Alert alert = new alerts.NetworkStatusSwitch_Alert();
                    alert.alert();
                }
                else if((bool)roamingSettings.Values["PulseLEDSwitch"] == true)
                {
                    alerts.NetworkStatusSwitch_Alert alert = new alerts.NetworkStatusSwitch_Alert();
                    alert.alert();

                    ((App)Application.Current).getLEDs().alertPulseLED();
                }
                else
                {
                    Debug.WriteLine("NetworkSwitch is still null.");
                }
            }
            else
            {
                Debug.WriteLine("Switch is off.");
            }
        }

        private void indAlert_Toggler()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values["PulseLEDSwitch"] == null || roamingSettings.Values["SmartThingsLowBatterySwitch"] == null || roamingSettings.Values["SmartThingsKnockSwitch"] == null || roamingSettings.Values["SmartThingsAwayDoorSwitch"] == null || roamingSettings.Values["SmartThingsEnviromentSwitch"] == null || roamingSettings.Values["SeverWeatherSwitch"] == null || roamingSettings.Values["PowerStatusSwitch"] == null || roamingSettings.Values["NetworkStatusSwitch"] == null)
            {
                Debug.WriteLine("One of the toggles is null.");
                roamingSettings.Values["PulseLEDSwitch"] = false;
                roamingSettings.Values["SmartThingsLowBatterySwitch"] = false;
                roamingSettings.Values["SmartThingsKnockSwitch"] = false;
                roamingSettings.Values["SmartThingsAwayDoorSwitch"] = false;
                roamingSettings.Values["SmartThingsEnviromentSwitch"] = false;
                roamingSettings.Values["SeverWeatherSwitch"] = false;
                roamingSettings.Values["PowerStatusSwitch"] = false;
                roamingSettings.Values["NetworkStatusSwitch"] = false;
            }
            else
            {
                bool PulseLEDValue = (bool)roamingSettings.Values["PulseLEDSwitch"];
                PulseLEDSwitch.IsOn = PulseLEDValue;

                bool SmartThingsLowBatteryValue = (bool)roamingSettings.Values["SmartThingsLowBatterySwitch"];
                SmartThingsLowBatterySwitch.IsOn = SmartThingsLowBatteryValue;

                bool SmartThingsKnockValue = (bool)roamingSettings.Values["SmartThingsKnockSwitch"];
                SmartThingsKnockSwitch.IsOn = SmartThingsKnockValue;

                bool SmartThingsAwayDoorValue = (bool)roamingSettings.Values["SmartThingsAwayDoorSwitch"];
                SmartThingsAwayDoorSwitch.IsOn = SmartThingsAwayDoorValue;

                bool SmartThingsEnviromentValue = (bool)roamingSettings.Values["SmartThingsEnviromentSwitch"];
                SmartThingsEnviromentSwitch.IsOn = SmartThingsEnviromentValue;

                bool SeverWeatherValue = (bool)roamingSettings.Values["SeverWeatherSwitch"];
                SeverWeatherSwitch.IsOn = SeverWeatherValue;

                bool PowerStatusValue = (bool)roamingSettings.Values["PowerStatusSwitch"];
                PowerStatusSwitch.IsOn = PowerStatusValue;

                bool NetworkStatusValue = (bool)roamingSettings.Values["NetworkStatusSwitch"];
                NetworkStatusSwitch.IsOn = NetworkStatusValue;
            }
        }
    }
}
