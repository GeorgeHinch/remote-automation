using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class SmartThingsAPI_GetDevices
    {
        #region Gets all  devices of TYPE in SmartThings
        public static List<string> getDeviceID(string type)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();

            List<string> deviceIDList = new List<string>();

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/data?access_token=" + token;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub[]));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        #region Adds device IDs to list
                        foreach (SmartThingsHub sth in devices)
                        {
                            bool typeString = false;
                            if (type == "music")
                            {
                                typeString = (sth.type == "music");
                            }
                            else if (type == "switch")
                            {
                                typeString = (sth.type == "switch");
                            }
                            else if (type == "motion")
                            {
                                typeString = (sth.type == "motion" || sth.type == "contact");
                            }
                            else if (type == "presence")
                            {
                                typeString = (sth.type == "presence");
                            }


                            if (sth.tile == "device" && typeString)
                            {
                                deviceIDList.Add(sth.device);
                            }
                        }
                        #endregion
                    }
                }
                return deviceIDList;
            }
            else { return null; }
        }
        #endregion

        #region Gets all  devices of TYPE in SmartThings
        public static List<SmartThingsHub> getDevice(string type)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();

            List<SmartThingsHub> deviceIDList = new List<SmartThingsHub>();

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/data?access_token=" + token;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub[]));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        #region Adds device IDs to list
                        foreach (SmartThingsHub sth in devices)
                        {
                            bool typeString = false;
                            if (type == "music")
                            {
                                typeString = (sth.type == "music");
                            }
                            else if (type == "switch")
                            {
                                typeString = (sth.type == "switch");
                            }
                            else if (type == "motion")
                            {
                                typeString = (sth.type == "motion" || sth.type == "contact");
                            }
                            else if (type == "presence")
                            {
                                typeString = (sth.type == "presence");
                            }


                            if (sth.tile == "device" && typeString)
                            {
                                deviceIDList.Add(sth);
                            }
                        }
                        #endregion
                    }
                }
                return deviceIDList;
            }
            else { return null; }
        }
        #endregion
    }
}
