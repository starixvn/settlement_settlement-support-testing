using System.Collections.Generic;

namespace WebSite.Models
{
    public class DeleteMatchRequest
    {
        public IEnumerable<int> MatchIds { get; set; }

        public int Userid { get; set; }
    }
}