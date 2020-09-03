using System.Collections.Generic;

namespace WebSite.Models
{
    public class CloneResultDetailModel
    {
        public List<ResultDetailModel> ResultDetails { get; set; }

        public List<ExternalResultDetailModel> ExternalResultDetails { get; set; }

        public int MatchId { get; set; }
    }
}