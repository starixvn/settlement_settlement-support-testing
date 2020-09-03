using System;
using System.Xml.Serialization;

namespace WebSite.Models
{
    public class ExternalResourcesItem
    {
        [XmlAttribute("MatchId3rd")]
        public int MatchId3rd { get; set; }

        [XmlAttribute("SourceId")]
        public string SourceId { get; set; }

        [XmlAttribute("ResourceId")]
        public string ResourceId { get; set; }

        [XmlAttribute("HomeValue")]
        public string HomeValue { get; set; }

        [XmlAttribute("AwayValue")]
        public string AwayValue { get; set; }

        [XmlAttribute("CreateDate")]
        public DateTime CreateDate { get; set; }

        [XmlAttribute("LastModifyDate")]
        public DateTime LastModifyDate { get; set; }

        [XmlAttribute("CreateBy")]
        public int CreateBy { get; set; }

        [XmlAttribute("LastModifyBy")]
        public int LastModifyBy { get; set; }

        [XmlAttribute("OtherInfo")]
        public string OtherInfo { get; set; }
    }
}