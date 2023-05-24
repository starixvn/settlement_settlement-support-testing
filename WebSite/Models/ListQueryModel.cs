using System.Collections.Generic;

namespace WebSite.Models
{
    public class ListQueryModel
    {
        public string SecurityToken { get; set; }

        public IList<Match> Matches { get; set; }
    }
}