using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class CloneResultDetailCriteria : NonQueryCommand
    {
        public int MatchId { get; set; }

        public string ResultDetail { get; set; }

        public string ExternalResult { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_Set_CloneResultDetailIns";
        }

        public override bool IsValid()
        {
            return MatchId > 0;
        }
    }
}