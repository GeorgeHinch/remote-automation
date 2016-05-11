using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class OWM_IconIndex
    {
        public static string getIcon(string description)
        {
            string icon;
            switch (description)
            {
                /// Thunderstorm
                case "thunderstorm with light rain":
                    icon = $"";
                    break;
                case "thunderstorm with rain":
                    icon = $"";
                    break;
                case "thunderstorm with heavy rain":
                    icon = $"";
                    break;
                case "light thunderstorm":
                    icon = $"";
                    break;
                case "thunderstorm":
                    icon = $"";
                    break;
                case "heavy thunderstorm":
                    icon = $"";
                    break;
                case "ragged thunderstorm":
                    icon = $"";
                    break;
                case "thunderstorm with light drizzle":
                    icon = $"";
                    break;
                case "thunderstorm with drizzle":
                    icon = $"";
                    break;
                case "thunderstorm with heavy drizzle":
                    icon = $"";
                    break;

                /// Drizzle
                case "light intensity drizzle":
                    icon = $"";
                    break;
                case "drizzle":
                    icon = $"";
                    break;
                case "heavy intensity drizzle":
                    icon = $"";
                    break;
                case "light intensity drizzle rain":
                    icon = $"";
                    break;
                case "drizzle rain":
                    icon = $"";
                    break;
                case "heavy intensity drizzle rain":
                    icon = $"";
                    break;
                case "shower rain and drizzle":
                    icon = $"";
                    break;
                case "heavy shower rain and drizzle":
                    icon = $"";
                    break;
                case "shower drizzle":
                    icon = $"";
                    break;

                /// Rain
                case "light rain":
                    icon = $"";
                    break;
                case "moderate rain":
                    icon = $"";
                    break;
                case "heavy intensity rain":
                    icon = $"";
                    break;
                case "very heavy rain":
                    icon = $"";
                    break;
                case "extreme rain":
                    icon = $"";
                    break;
                case "freezing rain":
                    icon = $"";
                    break;
                case "light intensity shower rain":
                    icon = $"";
                    break;
                case "shower rain":
                    icon = $"";
                    break;
                case "heavy intensity shower rain":
                    icon = $"";
                    break;
                case "ragged shower rain":
                    icon = $"";
                    break;

                /// Snow
                case "light snow":
                    icon = $"";
                    break;
                case "snow":
                    icon = $"";
                    break;
                case "heavy snow":
                    icon = $"";
                    break;
                case "sleet":
                    icon = $"";
                    break;
                case "shower sleet":
                    icon = $"";
                    break;
                case "light rain and snow":
                    icon = $"";
                    break;
                case "rain and snow":
                    icon = $"";
                    break;
                case "light shower snow":
                    icon = $"";
                    break;
                case "shower snow":
                    icon = $"";
                    break;
                case "heavy shower snow":
                    icon = $"";
                    break;

                /// Atmosphere
                case "mist":
                    icon = $"";
                    break;
                case "smoke":
                    icon = $"";
                    break;
                case "haze":
                    icon = $"";
                    break;
                case "sand, dust whirls":
                    icon = $"";
                    break;
                case "fog":
                    icon = $"";
                    break;
                case "sand":
                    icon = $"";
                    break;
                case "dust":
                    icon = $"";
                    break;
                case "volcanic ash":
                    icon = $"";
                    break;
                case "squalls":
                    icon = $"";
                    break;
                case "tornado":
                    icon = $"";
                    break;

                /// Clouds
                case "Sky is Clear":
                    icon = $"";
                    break;
                case "sky is clear":
                    icon = $"";
                    break;
                case "clear sky":
                    icon = $"";
                    break;
                case "few clouds":
                    icon = $"";
                    break;
                case "scattered clouds":
                    icon = $"";
                    break;
                case "broken clouds":
                    icon = $"";
                    break;
                case "overcast clouds":
                    icon = $"";
                    break;

                /// Extreme
                case "tropical storm":
                    icon = $"";
                    break;
                case "hurricane":
                    icon = $"";
                    break;
                case "cold":
                    icon = $"";
                    break;
                case "hot":
                    icon = $"";
                    break;
                case "windy":
                    icon = $"";
                    break;
                case "hail":
                    icon = $"";
                    break;

                /// Additional
                case "calm":
                    icon = $"";
                    break;
                case "light breeze":
                    icon = $"";
                    break;
                case "gentle breeze":
                    icon = $"";
                    break;
                case "moderate breeze":
                    icon = $"";
                    break;
                case "fresh breeze":
                    icon = $"";
                    break;
                case "strong breeze":
                    icon = $"";
                    break;
                case "high wind, near gale":
                    icon = $"";
                    break;
                case "gale":
                    icon = $"";
                    break;
                case "severe gale":
                    icon = $"";
                    break;
                case "storm":
                    icon = $"";
                    break;
                case "violent storm":
                    icon = $"";
                    break;

                /// Fallback
                default:
                    icon = $"";
                    break;
            }

            return icon;
        }
    }
}
