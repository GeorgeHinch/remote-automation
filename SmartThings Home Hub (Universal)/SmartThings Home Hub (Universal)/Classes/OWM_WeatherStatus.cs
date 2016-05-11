using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    class OWM_WeatherStatus
    {
        [DataContract]
        public class Coord
        {
            [DataMember]
            public double lon { get; set; }

            [DataMember]
            public double lat { get; set; }
        }

        [DataContract]
        public class Weather
        {
            [DataMember]
            public int id { get; set; }

            [DataMember]
            public string main { get; set; }

            [DataMember]
            public string description { get; set; }

            [DataMember]
            public string icon { get; set; }
        }

        [DataContract]
        public class Main
        {
            [DataMember]
            public double temp { get; set; }

            [DataMember]
            public double pressure { get; set; }

            [DataMember]
            public int humidity { get; set; }

            [DataMember]
            public double temp_min { get; set; }

            [DataMember]
            public double temp_max { get; set; }

            [DataMember]
            public double sea_level { get; set; }

            [DataMember]
            public double grnd_level { get; set; }
        }

        [DataContract]
        public class Wind
        {
            [DataMember]
            public double speed { get; set; }

            [DataMember]
            public double deg { get; set; }
        }

        [DataContract]
        public class Clouds
        {
            [DataMember]
            public int all { get; set; }
        }

        [DataContract]
        public class Sys
        {
            [DataMember]
            public double message { get; set; }

            [DataMember]
            public string country { get; set; }

            [DataMember]
            public int sunrise { get; set; }

            [DataMember]
            public int sunset { get; set; }
        }

        [DataContract]
        public class WeatherDetails
        {
            [DataMember]
            public Coord coord { get; set; }

            [DataMember]
            public IList<Weather> weather { get; set; }

            [DataMember(Name = "base")]
            public string weatherBase { get; set; }

            [DataMember]
            public Main main { get; set; }

            [DataMember]
            public Wind wind { get; set; }

            [DataMember]
            public Clouds clouds { get; set; }

            [DataMember]
            public int dt { get; set; }

            [DataMember]
            public Sys sys { get; set; }

            [DataMember]
            public int id { get; set; }

            [DataMember]
            public string name { get; set; }

            [DataMember]
            public int cod { get; set; }
        }
    }
}
