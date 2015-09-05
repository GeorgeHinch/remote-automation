using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SmartThings_Home_Hub__Universal_
{
    class SmartThingsAPI
    {
        /// Lights
        public static void SwitchesLoad(/*object sender, RoutedEventArgs e*/)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://graph.api.smartthings.com/api/smartapps/installations/5e726fc9-2569-4915-9af1-e1493524adf5/switches/?access_token=385f1cb1-9d53-4828-9cc3-931087483137");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.UTF8.GetBytes(result);

                

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var byteString = System.Text.Encoding.UTF8.GetString(bytes);
                    SwitchesDetails[] switchesDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<SwitchesDetails[]>(byteString);
                    
                    for (int i = 0; i < switchesDetails.Length; i++)
                    {
                        Debug.WriteLine(switchesDetails[i].label);
                        LightsPage.Light1.Text = switchesDetails[i].label;
                    }
                }
            }
        }

        public class SwitchesDetails
        {
            public String id { get; set; }
            public String label { get; set; }
            public String type { get; set; }
        }
    }
}
