using System.Xml.Serialization;
using System.Collections.Generic;

namespace WebSite.Models.ExternalResults
{
    [XmlRoot("Resources")]
    public class Resources
    {
        [XmlElement("Resource")]
        public List<ResultDetailResource> ResourceItems { get; set; }
    }
}