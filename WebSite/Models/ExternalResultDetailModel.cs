using System;

namespace WebSite.Models
{
    public class ExternalResultDetailModel
    {
        public int MatchId3rd { get; set; }

        public string SourceId { get; set; }

        public string ResourceId { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModifyDate { get; set; }

        public int CreateBy { get; set; }

        public int LastModifyBy { get; set; }

        public string OtherInfo { get; set; }
    }
}