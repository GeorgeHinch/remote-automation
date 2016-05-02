using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class SmartThingsAPI_Actions
    {
        #region Performs actions on SmartThings API
        public static void performAction(string type, string sender, string command, double value, bool isAll)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();
            List<string> devices = null;
            if (isAll)
            {
                devices = SmartThingsAPI_GetDevices.getDeviceID(type);
            }

            #region Actions for MUSIC type devices
            if (type == "music")
            {
                if (command == "play" || command == "pause")
                {
                    foreach (string device in devices)
                    {
                        string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&access_token=" + token;
                        HttpRequestMessage request = new HttpRequestMessage(
                            HttpMethod.Get,
                            rqstMsg);
                        HttpClient client = new HttpClient();
                        client.SendAsync(request);
                    }
                }
                else if (command == "nextTrack" || command == "previousTrack")
                {
                    string device = devices[0];
                    string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&access_token=" + token;
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Get,
                        rqstMsg);
                    HttpClient client = new HttpClient();
                    client.SendAsync(request);
                }
                else if (command == "level")
                {
                    foreach (string device in devices)
                    {
                        string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&value=" + value + "&access_token=" + token;
                        HttpRequestMessage request = new HttpRequestMessage(
                            HttpMethod.Get,
                            rqstMsg);
                        HttpClient client = new HttpClient();
                        client.SendAsync(request);
                    }
                }
            }
            #endregion

            #region Actions for SWITCH type devices
            if (type == "switch")
            {
                if (!isAll)
                {
                    string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=switch&device=" + sender + "&command=" + command + "&access_token=" + token;
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Get,
                        rqstMsg);
                    HttpClient client = new HttpClient();
                    client.SendAsync(request);
                }
                else
                {
                    foreach (string device in devices)
                    {
                        string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=switch&device=" + device + "&command=" + command + "&access_token=" + token;
                        HttpRequestMessage request = new HttpRequestMessage(
                            HttpMethod.Get,
                            rqstMsg);
                        HttpClient client = new HttpClient();
                        client.SendAsync(request);
                    }
                }
            }
            #endregion

            #region Actions for LOCK type devices
            if (type == "lock")
            {
                if (!isAll)
                {
                    string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=lock&device=" + sender + "&command=" + command + "&access_token=" + token;
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Get,
                        rqstMsg);
                    HttpClient client = new HttpClient();
                    client.SendAsync(request);
                }
                else
                {
                    foreach (string device in devices)
                    {
                        string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=lock&device=" + device + "&command=" + command + "&access_token=" + token;
                        HttpRequestMessage request = new HttpRequestMessage(
                            HttpMethod.Get,
                            rqstMsg);
                        HttpClient client = new HttpClient();
                        client.SendAsync(request);
                    }
                }
            }
            #endregion

            #region Actions for THERMOSTAT type devices
            if (type == "thermostatHeat" || type == "thermostatCool")
            {

            }
            #endregion

            #region Actions for CAMERA type devices
            if (type == "camera")
            {

            }
            #endregion

            #region Actions for MODE type
            if (type == "mode")
            {
                string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=mode&device=mode&command=" + command.Replace(" ", "+") + "&access_token=" + token;
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
                HttpClient client = new HttpClient();
                client.SendAsync(request);
            }
            #endregion
        }
        #endregion
    }
}
