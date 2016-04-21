using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    [DataContract]
    class LastFM_SongFind
    {
        [DataMember]
        public Track track { get; set; }
    }

    [DataContract]
    public class Streamable
    {
        [DataMember(Name = "#text")]
        public string text { get; set; }

        [DataMember]
        public string fulltrack { get; set; }
    }

    [DataContract]
    public class Artist
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string mbid { get; set; }

        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class SongImage
    {
        [DataMember(Name = "#text")]
        public string text { get; set; }

        [DataMember]
        public string size { get; set; }
    }

    [DataContract]
    public class Attr
    {
        [DataMember]
        public string position { get; set; }
    }

    [DataContract]
    public class Album
    {
        [DataMember]
        public string artist { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string mbid { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public IList<SongImage> image { get; set; }

        [DataMember]
        public Attr @attr { get; set; }
    }

    [DataContract]
    public class Tag
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    public class Toptags
    {
        [DataMember]
        public IList<Tag> tag { get; set; }
    }

    [DataContract]
    public class Track
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string mbid { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string duration { get; set; }

        [DataMember]
        public Streamable streamable { get; set; }

        [DataMember]
        public string listeners { get; set; }

        [DataMember]
        public string playcount { get; set; }

        [DataMember]
        public Artist artist { get; set; }

        [DataMember]
        public Album album { get; set; }

        [DataMember]
        public Toptags toptags { get; set; }
    }
}
