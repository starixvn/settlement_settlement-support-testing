using Fanex.Data.Repository;
using WebSite.Constants;

namespace WebSite.Criteria
{
    public class AddSoccerWayResultCriteria : NonQueryCommand
    {
        public int MatchId { get; set; }

        public int SourceId { get; set; }

        public GoalType TypeId { get; set; }

        public string HomeValue { get; set; }

        public string AwayValue { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_Set_AutoPayout_ProviderResultDetailUpd";
        }

        public override bool IsValid()
        {
            return MatchId > 0;
        }
    }
}