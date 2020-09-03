using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class GetResultDetailCriteria : CriteriaBase
    {
        public int MatchId { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_Set_CloneResultDetailSel";
        }

        public override bool IsValid()
        {
            return MatchId > 0;
        }
    }
}