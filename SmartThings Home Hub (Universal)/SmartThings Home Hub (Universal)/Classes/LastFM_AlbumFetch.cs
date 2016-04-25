using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    class LastFM_AlbumFetch
    {
        #region Fetches artwork from lastFM
        public static string fetchLastFMArtwork(string song, string artist)
        {
            string k = "73bd84173ee30b7e3d5e55c1c73f672a";
            string imgBMP = null;

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;


            string rqstMsg = "http://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key=" + k + "&artist=" + Uri.EscapeDataString(artist) + "&track=" + Uri.EscapeDataString(song) + "&format=json";
            Debug.WriteLine("Song URL: " + rqstMsg + " |");

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
                        try
                        {
                            var serializer = new DataContractJsonSerializer(typeof(LastFM_SongFind));
                            LastFM_SongFind foundSong = (LastFM_SongFind)serializer.ReadObject(stream);
                            IList<SongImage> image = (IList<SongImage>)foundSong.track.album.image;

                            #region Gets and sets album info from JSON
                            foreach (SongImage img in image)
                            {
                                if (img.size == "extralarge")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "large")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "medium")
                                {
                                    imgBMP = img.text;
                                }
                                else if (img.size == "small")
                                {
                                    imgBMP = img.text;
                                }
                                else
                                {
                                    imgBMP = null;
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Album Artwork Exception: " + ex.Message + " |");
                            return null;
                        }
                    }
                }
            }
            return imgBMP;
        }
        #endregion

    }
}
