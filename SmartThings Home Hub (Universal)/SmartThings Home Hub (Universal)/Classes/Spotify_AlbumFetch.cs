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
    class Spotify_AlbumFetch
    {
        #region Fetches artwork from Spotify
        public static string fetchSpotifyArtwork(string song, string artist)
        {
            string imgBMP = null;

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;


            string rqstMsg = "https://api.spotify.com/v1/search?q=artist:" + Uri.EscapeDataString(artist) + "%20track:" + Uri.EscapeDataString(song) + "&offset=0&limit=1&type=track";
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
                            var serializer = new DataContractJsonSerializer(typeof(Spotify_SongFind));
                            Spotify_SongFind foundSong = (Spotify_SongFind)serializer.ReadObject(stream);
                            Spotify_Item sItem = foundSong.tracks.items.Last();
                            IList<Spotify_Image> images = sItem.album.images;

                            #region Gets and sets album info from JSON
                            foreach (Spotify_Image img in images)
                            {
                                if (img.height == 640)
                                {
                                    return img.url;
                                }
                                else if (img.height == 300)
                                {
                                    return img.url;
                                }
                                else
                                {
                                    return img.url;
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
