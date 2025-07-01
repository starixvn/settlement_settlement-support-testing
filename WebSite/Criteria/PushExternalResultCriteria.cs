using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class PushExternalResultCriteria : NonQueryCommand
    {
        public int MatchId3rd { get; set; }

        public int MatchId { get; set; }

        public int SourceId { get; set; }

        public string Resources { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_Set_ExternalResult_Ins";
        }

        public override bool IsValid()
        {
            return MatchId > 0;
        }
    }
}