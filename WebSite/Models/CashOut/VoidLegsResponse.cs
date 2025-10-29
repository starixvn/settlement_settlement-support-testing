namespace WebSite.Models.CashOut
{
    public class VoidLegsResponse
    {
        public VoidLegsHeader Header { get; set; }
    }

    public class VoidLegsHeader
    {
        public string ReplyStatusCode { get; set; }
    }
}

