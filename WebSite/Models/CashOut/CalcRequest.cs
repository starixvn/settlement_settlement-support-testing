namespace WebSite.Models.CashOut
{
    public class CalcRequest
    {
        public int MatchId { get; set; }

        public string Betstatus { get; set; }

        public string[] LsHalf { get; set; }
    }
}