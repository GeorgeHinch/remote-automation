using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    public class Spotify_SongFind
    {
        [DataMember]
        public Tracks tracks { get; set; }
    }

    [DataContract]
    public class ExternalUrls
    {
        [DataMember]
        public string spotify { get; set; }
    }

    [DataContract]
    public class Spotify_Image
    {
        [DataMember]
        public int height { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public int width { get; set; }
    }

    [DataContract]
    public class Spotify_Album
    {
        [DataMember]
        public string album_type { get; set; }

        [DataMember]
        public IList<string> available_markets { get; set; }

        [DataMember]
        public ExternalUrls external_urls { get; set; }

        [DataMember]
        public string href { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public IList<Spotify_Image> images { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string uri { get; set; }
    }

    [DataContract]
    public class Spotify_Artist
    {
        [DataMember]
        public ExternalUrls external_urls { get; set; }

        [DataMember]
        public string href { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string uri { get; set; }
    }

    [DataContract]
    public class ExternalIds
    {
        [DataMember]
        public string isrc { get; set; }
    }

    [DataContract]
    public class Spotify_Item
    {
        [DataMember]
        public Spotify_Album album { get; set; }

        [DataMember]
        public IList<Spotify_Artist> artists { get; set; }

        [DataMember]
        public IList<string> available_markets { get; set; }

        [DataMember]
        public int disc_number { get; set; }

        [DataMember]
        public int duration_ms { get; set; }

        [DataMember]
        public bool Explicit { get; set; }

        [DataMember]
        public ExternalIds external_ids { get; set; }

        [DataMember]
        public ExternalUrls external_urls { get; set; }

        [DataMember]
        public string href { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int popularity { get; set; }

        [DataMember]
        public string preview_url { get; set; }

        [DataMember]
        public int track_number { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string uri { get; set; }
    }

    [DataContract]
    public class Tracks
    {
        [DataMember]
        public string href { get; set; }

        [DataMember]
        public IList<Spotify_Item> items { get; set; }

        [DataMember]
        public int limit { get; set; }

        [DataMember]
        public object next { get; set; }

        [DataMember]
        public int offset { get; set; }

        [DataMember]
        public object previous { get; set; }

        [DataMember]
        public int total { get; set; }
    }
}
