using Fanex.Data.Repository;

namespace WebSite.Criteria
{
    public class InsertMatchScoresCriteria : NonQueryCommand
    {
        public int SourceId { get; set; }

        public string ResourceXML { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_Set_AutoInstant_ProviderResultDetailIns";
        }

        public override bool IsValid()
        {
            return SourceId >= 0;
        }
    }
}