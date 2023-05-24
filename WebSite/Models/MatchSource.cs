using System;

namespace WebSite.Models
{
    public class MatchSource
    {
        public int MatchId { get; set; }

        public int Source { get; set; }

        public int? HalfTimeHomeScore { get; set; }

        public int? HalfTimeAwayScore { get; set; }

        public DateTime HalfTimeCreatedDate { get; set; }

        public int? FullTimeHomeScore { get; set; }

        public int? FullTimeAwayScore { get; set; }

        public DateTime FullTimeCreatedDate { get; set; }
    }
}