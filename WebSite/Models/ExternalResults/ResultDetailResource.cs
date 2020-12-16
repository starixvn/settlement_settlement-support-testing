using System.Xml.Serialization;

namespace WebSite.Models.ExternalResults
{
    public class ResultDetailResource
    {
        [XmlAttribute("Id")]
        public string ResourceId { get; set; }

        [XmlAttribute("Home")]
        public string HomeValue { get; set; }

        [XmlAttribute("Away")]
        public string AwayValue { get; set; }
    }
}