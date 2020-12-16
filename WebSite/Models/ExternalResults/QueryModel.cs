using System.Collections.Generic;

namespace WebSite.Models.ExternalResults
{
    public class QueryModel
    {
        public int MatchId3rd { get; set; }

        public int MatchId { get; set; }

        public int SourceId { get; set; }

        public List<ResultDetailResource> Results { get; set; }
    }
}