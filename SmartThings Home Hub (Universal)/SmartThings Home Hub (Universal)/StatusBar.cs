using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace SmartThings_Home_Hub__Universal_
{
    public sealed class StatusBar : Control
    {
        public StatusBar()
        {
            this.DefaultStyleKey = typeof(StatusBar);

            /*DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            EventHandler<Object> stupd = new EventHandler<object>(this.timer_Tick);
            timer.Tick += stupd;
            timer.Start();*/
        }

        /*async void timer_Tick(object sender, object something)
        {
            //var wifiAdapters = await WiFiAdapter.FindAllAdaptersAsync();
            //var firstWifiAdapter = wifiAdapters[0]; // be more careful, check size, etc...
            //Debug.WriteLine(wifiAdapters[0]);       // again, check size, or look for your specific network
            //var rssi = firstWifiAdapter.NetworkReport.AvailableNetworks[0].NetworkRssiInDecibelMilliwatts;

            this.pwrIcon(this.getCurrentPowerSupplyStatus());

        }

        public PowerSupplyStatus getCurrentPowerSupplyStatus()
        {
            return Windows.System.Power.PowerManager.PowerSupplyStatus;
        }

        //public static readonly DependencyProperty pwrStateProperty = DependencyProperty.Register("pwrState", typeof(string), typeof(StatusBar), new PropertyMetadata("1"));

        public void pwrIcon(PowerSupplyStatus status)
        {
            switch (status)
            {
                case PowerSupplyStatus.Inadequate:
                    //strPowerLineStatus = "Device is running on battery";

                    //MyValueProperty = DependencyProperty.Register("pwrState", typeof(string), typeof(StatusBar), new PropertyMetadata("2"));
                    //pwrState = "2";

                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.Adequate:
                    //strPowerLineStatus = "Device is plugged in";

                    //MyValueProperty = DependencyProperty.Register("pwrState", typeof(string), typeof(StatusBar), new PropertyMetadata("3"));
                    //pwrState = "3";

                    //pwrStateIcon.Text = $"";
                    break;
                case PowerSupplyStatus.NotPresent:
                    //strPowerLineStatus = "Power Status: Unknown";

                    //MyValueProperty = DependencyProperty.Register("pwrState", typeof(string), typeof(StatusBar), new PropertyMetadata("4"));
                    //pwrState = "4";

                    //pwrStateIcon.Text = $"";
                    break;
            }
        }

        public static string pwrState;
        {
            get { return (string)GetValue(pwrStateProperty); }
            set { SetValue(pwrStateProperty, value); }
        }*/
    }
}
