using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class Perform_MusicAction
    {
        #region Performs actions on SmartThings API
        private void performMusicAction(string command)
        {
            string app = SmartThingsAPI_Access.getApp();
            string token = SmartThingsAPI_Access.getToken();
            List<string> devices = SmartThingsAPI_GetDevices.getDevice(app, token, "music");

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
            else
            {
                string device = devices[0];
                string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/command?type=music&device=" + device + "&command=" + command + "&access_token=" + token;
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
                HttpClient client = new HttpClient();
                client.SendAsync(request);
            }
        }
        #endregion
    }
}
