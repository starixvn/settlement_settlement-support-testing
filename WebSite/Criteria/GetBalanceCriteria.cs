using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class GetBalanceCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int MatchId { get; set; }

        public override bool IsValid() => true;

        public override string GetSettingKey() => "DBA_BalanceMemberSel";
    }
}