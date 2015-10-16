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
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LEDsSettings : Page
    {
        public LEDsSettings()
        {
            this.InitializeComponent();

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load LED power toggle state */
            if (roamingSettings.Values["ledPower"] != null)
            {
                object settingValue = roamingSettings.Values["ledPower"];
                ledPower.IsOn = (bool)settingValue;
            }
            else
            {
                Debug.WriteLine("You fucked up.");
                return;
            }

            ledRadio_Checker();
        }

        private void FadeLED_Checked(object sender, RoutedEventArgs e)
        {
            ledRadioGroup_IsChecked();
            ((App)Application.Current).getLEDs().fadeTogether();
        }

        private void BlueLED_Checked(object sender, RoutedEventArgs e)
        {
            ledRadioGroup_IsChecked();
            ((App)Application.Current).getLEDs().setColorAll(0, 0, 255);
        }

        private void WhiteLED_Checked(object sender, RoutedEventArgs e)
        {
            ledRadioGroup_IsChecked();
            ((App)Application.Current).getLEDs().setColorAll(255, 255, 255);
        }

        private void RainbowLED_Checked(object sender, RoutedEventArgs e)
        {
            ledRadioGroup_IsChecked();
            ((App)Application.Current).getLEDs().rainbowTogether();
        }
        
        private void ledRadioGroup_IsChecked()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            RadioButton ledState = null;

            if ((bool)WhiteLED.IsChecked)
            {
                ledState = WhiteLED;
                Debug.WriteLine("Wrote whiteLED");
            }
            else if ((bool)BlueLED.IsChecked)
            {
                ledState = BlueLED;
                Debug.WriteLine("Wrote blueLED");
            }
            else if ((bool)FadeLED.IsChecked)
            {
                ledState = FadeLED;
                Debug.WriteLine("Wrote fadeLED");
            }
            else if ((bool)RainbowLED.IsChecked)
            {
                ledState = RainbowLED;
                Debug.WriteLine("Wrote rainbowLed");
            }
            else
            {
                ledState = null;
            }

            Debug.WriteLine("ledState.ToString() = " + ledState.Name.ToString() + "|");
            roamingSettings.Values["ledRadio"] = ledState.Name.ToString();
        }

        public void ledRadio_Checker()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Load LED color radio state */
            if ((string) roamingSettings.Values["ledRadio"] == WhiteLED.Name.ToString())
            {
                WhiteLED.IsChecked = true;
            }
            else if ((string) roamingSettings.Values["ledRadio"] == BlueLED.Name.ToString())
            {
                BlueLED.IsChecked = true;
            }
            else if ((string) roamingSettings.Values["ledRadio"] == FadeLED.Name.ToString())
            {
                FadeLED.IsChecked = true;
            }
            else if ((string) roamingSettings.Values["ledRadio"] == RainbowLED.Name.ToString())
            {
                RainbowLED.IsChecked = true;
            }
            else
            {
                Debug.WriteLine("The if statement is fucked up.");
            }
        }

        private void ledPower_Toggled(object sender, RoutedEventArgs e)
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            /* Store LED on/off status */
            roamingSettings.Values["ledPower"] = ledPower.IsOn;

            ledPower_Checker();
            
        }

        public void ledPower_Checker()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            bool settingValue = (bool)roamingSettings.Values["ledPower"];


            if (settingValue == true)
            {
                ledRadio_Checker();
            }
            else if (settingValue == false)
            {
                ledRadio_Disable();
                ((App)Application.Current).getLEDs().turnOffLED();
            }
        }

        private void ledRadio_Disable()
        {
            WhiteLED.IsEnabled = false;
            BlueLED.IsEnabled = false;
            FadeLED.IsEnabled = false;
            RainbowLED.IsEnabled = false;
        }
    }
}
