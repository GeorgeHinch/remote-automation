using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_.Classes
{
    [DataContract]
    class NYT_TopStories
    {
        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string copyright { get; set; }

        [DataMember]
        public string section { get; set; }

        [DataMember]
        public string last_updated { get; set; }

        [DataMember]
        public int num_results { get; set; }

        [DataMember]
        public IList<NYT_Result> results { get; set; }
    }

    [DataContract]
    class NYT_Result
    {
        [DataMember]
        public string section { get; set; }

        [DataMember]
        public string subsection { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember(Name = "abstract")]
        public string story_abstract { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string byline { get; set; }

        [DataMember]
        public string item_type { get; set; }

        [DataMember]
        public string updated_date { get; set; }

        [DataMember]
        public string created_date { get; set; }

        [DataMember]
        public string published_date { get; set; }

        [DataMember]
        public string material_type_facet { get; set; }

        [DataMember]
        public string kicker { get; set; }

        [DataMember]
        public object des_facet { get; set; }

        [DataMember]
        public object org_facet { get; set; }

        [DataMember]
        public object per_facet { get; set; }

        [DataMember]
        public object geo_facet { get; set; }

        [DataMember]
        public IList<Multimedia> multimedia { get; set; }
    }

    [DataContract]
    class Multimedia
    {
        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string format { get; set; }

        [DataMember]
        public int height { get; set; }

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string subtype { get; set; }

        [DataMember]
        public string caption { get; set; }

        [DataMember]
        public string copyright { get; set; }

    }
}
