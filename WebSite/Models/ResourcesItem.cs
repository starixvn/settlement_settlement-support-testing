using System;
using System.Xml.Serialization;

namespace WebSite.Models
{
    public class ResourcesItem
    {
        [XmlAttribute("ResourceId")]
        public string ResourceId { get; set; }

        [XmlAttribute("HomeValue")]
        public string HomeValue { get; set; }

        [XmlAttribute("AwayValue")]
        public string AwayValue { get; set; }

        [XmlAttribute("ResultStatusId")]
        public byte ResultStatusId { get; set; }

        [XmlAttribute("SettlementStatusId")]
        public byte SettlementStatusId { get; set; }

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