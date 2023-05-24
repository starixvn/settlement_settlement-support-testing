using System.Xml.Serialization;

namespace WebSite.Models
{
    public class AutoInstantScore
    {
        [XmlAttribute]
        public short Type { get; set; }

        [XmlAttribute]
        public string HomeValue { get; set; }

        [XmlAttribute]
        public string AwayValue { get; set; }
    }
}