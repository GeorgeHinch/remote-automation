using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static SmartThings_Home_Hub__Universal_.Classes.OWM_WeatherStatus;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class OWM_GetWeather
    {
        public static WeatherDetails getSingleCity()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (roamingSettings.Values["ZipCode"] == null)
            {
                roamingSettings.Values["ZipCode"] = "98122";
            }

            string zip = (string)roamingSettings.Values["ZipCode"];
            string country = "US";

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.openweathermap.org/data/2.5/weather?zip={zip},{country}&units=imperial&APPID=e1e2647eeddb5412d5c4ee2fef620871");
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

                    return weatherDetails;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
