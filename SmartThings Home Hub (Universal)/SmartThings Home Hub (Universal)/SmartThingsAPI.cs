using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SmartThings_Home_Hub__Universal_
{
    class SmartThingsAPI
    {
        /// Lights
        public async void SwitchesLoad(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/5e726fc9-2569-4915-9af1-e1493524adf5/switches/?access_token=385f1cb1-9d53-4828-9cc3-931087483137");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Switches));
                    var weatherDetails = (Switches)serializer.ReadObject(stream);

                    for (int i = 0; i < Switches.id.Length; i++)
                    {
                        Debug.WriteLine(Switches[i]);
                    }
                    LightsPage.Light1.Text = Switches.id;
                }
            }
        }

        public class SwitchesDetails
        {
            public string id { get; set; }
            public string label { get; set; }
            public string type { get; set; }
        }

        public class Switches
        {
            public SwitchesDetails main { get; set; }
        }


        /// Locks
        public async void LocksLoad(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/5e726fc9-2569-4915-9af1-e1493524adf5/locks/?access_token=385f1cb1-9d53-4828-9cc3-931087483137");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Locks));
                    var lockDetails = (Locks)serializer.ReadObject(stream);

                    ///things to do with locks
                }
            }
        }

        public class LocksDetails
        {
            public string id { get; set; }
            public string label { get; set; }
            public string type { get; set; }
        }

        public class Locks
        {
            public SwitchesDetails main { get; set; }
        }

    }
}
