using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SmartThings_Home_Hub__Universal_.Classes;
using static SmartThings_Home_Hub__Universal_.Classes.OWM_WeatherStatus;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        public WeatherPage()
        {
            this.InitializeComponent();

            WeatherDetails weatherDetails = OWM_GetWeather.getSingleCity();

            if (weatherDetails != null)
            {
                WeatherLocation.Text = weatherDetails.name + ", " + weatherDetails.sys.country;

                WeatherTemp.Text = Math.Round(weatherDetails.main.temp) + "°";
                WeatherLow.Text = "« " + Math.Round(weatherDetails.main.temp_min) + "°";
                WeatherHigh.Text = Math.Round(weatherDetails.main.temp_max) + "° »";

                WeatherIcon.Text = OWM_IconIndex.getIcon(weatherDetails.weather[0].description);
            }
            else
            {
                WeatherTemp.Text = "err";
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
