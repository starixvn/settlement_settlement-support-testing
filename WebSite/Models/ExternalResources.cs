using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebSite.Models
{
    [XmlRoot("Results")]
    public class ExternalResources
    {
        [XmlElement("Result")]
        public List<ExternalResourcesItem> ResourceItems { get; set; }
    }
}