using System.Xml.Serialization;

namespace WebSite.Models
{
    [XmlRoot("Match")]
    public class AutoInstantMatch
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlElement("Score")]
        public AutoInstantScore[] Scores { get; set; }
    }
}