using System.Collections.Generic;

namespace WebSite.Models
{
    public class QueryModel
    {
        public string SecurityToken { get; set; }

        public int MatchId { get; set; }

        public List<Score> ResultInfo { get; set; }
    }
}