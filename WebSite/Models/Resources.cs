using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebSite.Models
{
    [XmlRoot("Results")]
    public class Resources
    {
        [XmlElement("Result")]
        public List<ResourcesItem> ResourceItems { get; set; }
    }
}