using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            string zip = (string)roamingSettings.Values["ZipCode"];

            string city = "Seattle";
            string country = "US";

            if (roamingSettings.Values["ZipCode"] == null)
            {
                roamingSettings.Values["ZipCode"] = "98122";
            } else
            {
                WeatherLocation.Text = $"{city}, {country}";
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.openweathermap.org/data/2.5/weather?zip={zip},{country}&APPID=e1e2647eeddb5412d5c4ee2fef620871");
                /* $"http://api.openweathermap.org/data/2.5/weather?q={city},{country}&APPID=e1e2647eeddb5412d5c4ee2fef620871"); */
                HttpClient client = new HttpClient();
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(WeatherDetails));
                        var weatherDetails = (WeatherDetails)serializer.ReadObject(stream);

                        WeatherTemp.Text = $"{(weatherDetails.main.temp - 273.15f) * 1.800 + 32:F0}°";
                        WeatherLow.Text = $"« {(weatherDetails.main.temp_min - 273.15f) * 1.800 + 32:F0}°";
                        WeatherHigh.Text = $"{(weatherDetails.main.temp_max - 273.15f) * 1.800 + 32:F0}° »";
                    }

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(WeatherStatus));
                        var weatherStatus = (WeatherStatus)serializer.ReadObject(stream);

                        switch (weatherStatus.weather[0].description)
                        {
                            /// Thunderstorm
                            case "thunderstorm with light rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm with rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm with heavy rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "light thunderstorm":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy thunderstorm":
                                WeatherIcon.Text = $"";
                                break;
                            case "ragged thunderstorm":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm with light drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm with drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "thunderstorm with heavy drizzle":
                                WeatherIcon.Text = $"";
                                break;

                            /// Drizzle
                            case "light intensity drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy intensity drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "light intensity drizzle rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "drizzle rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy intensity drizzle rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "shower rain and drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy shower rain and drizzle":
                                WeatherIcon.Text = $"";
                                break;
                            case "shower drizzle":
                                WeatherIcon.Text = $"";
                                break;

                            /// Rain
                            case "light rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "moderate rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy intensity rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "very heavy rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "extreme rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "freezing rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "light intensity shower rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "shower rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy intensity shower rain":
                                WeatherIcon.Text = $"";
                                break;
                            case "ragged shower rain":
                                WeatherIcon.Text = $"";
                                break;

                            /// Snow
                            case "light snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "sleet":
                                WeatherIcon.Text = $"";
                                break;
                            case "shower sleet":
                                WeatherIcon.Text = $"";
                                break;
                            case "light rain and snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "rain and snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "light shower snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "shower snow":
                                WeatherIcon.Text = $"";
                                break;
                            case "heavy shower snow":
                                WeatherIcon.Text = $"";
                                break;

                            /// Atmosphere
                            case "mist":
                                WeatherIcon.Text = $"";
                                break;
                            case "smoke":
                                WeatherIcon.Text = $"";
                                break;
                            case "haze":
                                WeatherIcon.Text = $"";
                                break;
                            case "sand, dust whirls":
                                WeatherIcon.Text = $"";
                                break;
                            case "fog":
                                WeatherIcon.Text = $"";
                                break;
                            case "sand":
                                WeatherIcon.Text = $"";
                                break;
                            case "dust":
                                WeatherIcon.Text = $"";
                                break;
                            case "volcanic ash":
                                WeatherIcon.Text = $"";
                                break;
                            case "squalls":
                                WeatherIcon.Text = $"";
                                break;
                            case "tornado":
                                WeatherIcon.Text = $"";
                                break;

                            /// Clouds
                            case "Sky is Clear":
                                WeatherIcon.Text = $"";
                                break;
                            case "sky is clear":
                                WeatherIcon.Text = $"";
                                break;
                            case "clear sky":
                                WeatherIcon.Text = $"";
                                break;
                            case "few clouds":
                                WeatherIcon.Text = $"";
                                break;
                            case "scattered clouds":
                                WeatherIcon.Text = $"";
                                break;
                            case "broken clouds":
                                WeatherIcon.Text = $"";
                                break;
                            case "overcast clouds":
                                WeatherIcon.Text = $"";
                                break;

                            /// Extreme
                            case "tropical storm":
                                WeatherIcon.Text = $"";
                                break;
                            case "hurricane":
                                WeatherIcon.Text = $"";
                                break;
                            case "cold":
                                WeatherIcon.Text = $"";
                                break;
                            case "hot":
                                WeatherIcon.Text = $"";
                                break;
                            case "windy":
                                WeatherIcon.Text = $"";
                                break;
                            case "hail":
                                WeatherIcon.Text = $"";
                                break;

                            /// Additional
                            case "calm":
                                WeatherIcon.Text = $"";
                                break;
                            case "light breeze":
                                WeatherIcon.Text = $"";
                                break;
                            case "gentle breeze":
                                WeatherIcon.Text = $"";
                                break;
                            case "moderate breeze":
                                WeatherIcon.Text = $"";
                                break;
                            case "fresh breeze":
                                WeatherIcon.Text = $"";
                                break;
                            case "strong breeze":
                                WeatherIcon.Text = $"";
                                break;
                            case "high wind, near gale":
                                WeatherIcon.Text = $"";
                                break;
                            case "gale":
                                WeatherIcon.Text = $"";
                                break;
                            case "severe gale":
                                WeatherIcon.Text = $"";
                                break;
                            case "storm":
                                WeatherIcon.Text = $"";
                                break;
                            case "violent storm":
                                WeatherIcon.Text = $"";
                                break;

                            /// Fallback
                            default:
                                WeatherIcon.Text = $"";
                                break;
                        }
                    }
                }
                else
                {
                    WeatherTemp.Text = "err";
                }
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
        }

        public class WeatherDetails
        {
            public Temperature main { get; set; }
        }

        public class WeatherStatus
        {
            public int id { get; set; }
            public String name { get; set; }
            public int cod { get; set; }
            public WeatherCondition[] weather { get; set; }
        }

        public class WeatherCondition
        {
            public int id { get; set; }
            public String description { get; set; }
            public String main { get; set; }
            public String icon { get; set; }
        }

        private async void ZipNull_Err()
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog("No ZIP code has been entered. Enter ZIP now or visit the settings page later.");
            // Add commands and set their callbacks
            messageDialog.Commands.Add(new UICommand("Home", (command) =>
            {
                this.Frame.Navigate(typeof(HomePage));
            }));
            messageDialog.Commands.Add(new UICommand("Enter Zip", (command) =>
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }));
            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;
            // Show the message dialog
            await messageDialog.ShowAsync();
        }
    }
}
