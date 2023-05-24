using System;

namespace WebSite.Models
{
    public class ScoreViewModel
    {
        public int SourceId { get; set; }

        public int TypeId { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}