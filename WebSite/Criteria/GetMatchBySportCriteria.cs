using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class GetMatchBySportCriteria : CriteriaBase
    {
        public int SportType { get; set; }

        public override string GetSettingKey()
        {
            return "Kaden_GetMatchForTesting";
        }

        public override bool IsValid()
        {
            return SportType > 0;
        }
    }
}