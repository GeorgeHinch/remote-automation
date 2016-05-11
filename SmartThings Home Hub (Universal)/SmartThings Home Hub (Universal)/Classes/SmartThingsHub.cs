using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    [DataContract]
    public class SmartThingsHub
    {
        [DataMember(Name = "tile")]
        public string tile { get; set; }

        [DataMember(Name = "size")]
        public int size { get; set; }

        [DataMember(Name = "style")]
        public string style { get; set; }

        [DataMember(Name = "date")]
        public string date { get; set; }

        [DataMember(Name = "dow")]
        public string dow { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "mode")]
        public string mode { get; set; }

        [DataMember(Name = "isStandardMode")]
        public bool? isStandardMode { get; set; }

        [DataMember(Name = "modes")]
        public IList<string> modes { get; set; }

        [DataMember(Name = "phrases")]
        public IList<string> phrases { get; set; }

        [DataMember(Name = "device")]
        public string device { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "level")]
        public int? level { get; set; }

        [DataMember(Name = "trackDescription")]
        public object trackDescription { get; set; }

        [DataMember(Name = "mute")]
        public bool? mute { get; set; }

        [DataMember(Name = "active")]
        public string active { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }

        [DataMember(Name = "isValue")]
        public bool? isValue { get; set; }

        [DataMember(Name = "ts")]
        public string ts { get; set; }

        [DataMember(Name = "humidity")]
        public string humidity { get; set; }

        [DataMember(Name = "temperature")]
        public string temperature { get; set; }

        [DataMember(Name = "thermostatFanMode")]
        public string thermostatFanMode { get; set; }

        [DataMember(Name = "thermostatOperatingState")]
        public string thermostatOperatingState { get; set; }

        [DataMember(Name = "setpoint")]
        public int setpoint { get; set; }
    }
}
