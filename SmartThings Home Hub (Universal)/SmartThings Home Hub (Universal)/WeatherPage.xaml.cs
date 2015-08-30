using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WUnderground.Client;

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

            string city = "Seattle";
            string country = "US";

            WeatherLocation.Text = $"{city},{country}";
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"http://api.openweathermap.org/data/2.5/weather?q={city},{country}");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Weather));
                    var weather = (Weather)serializer.ReadObject(stream);
                    WeatherTemp.Text = $"{(weather.main.temp - 273.15f) * 1.800 + 32:F0}°";
                    WeatherLow.Text = $"« {(weather.main.temp_min - 273.15f) * 1.800 + 32:F0}°";
                    WeatherHigh.Text = $"{(weather.main.temp_max - 273.15f) * 1.800 + 32:F0}° »";
                    switch (weather.main.description)
                    {
                        case "light rain":
                            WeatherIcon.Text = $"&#61561;";
                            break;
                        case "":
                            WeatherIcon.Text = $"";
                            break;
                        case " ":
                            WeatherIcon.Text = $"";
                            break;
                    }
                }
            }
            else
            {
                WeatherTemp.Text = "Err";
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        public class Temperature
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public string description { get; set; }
    }

        public class Weather
        {
            public Temperature main { get; set; }
        }
    }
}

