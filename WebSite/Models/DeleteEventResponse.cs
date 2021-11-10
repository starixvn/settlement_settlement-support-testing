namespace WebSite.Models
{
    public class DeleteEventResponse
    {
        public DeleteEventResponse(int matchid, bool deleteEventSuccess, string mrg)
        {
            Matchid = matchid;
            DeleteEventMsg = mrg;
            DeleteEventSuccess = deleteEventSuccess;
        }

        public int Matchid { get; set; }

        public bool DeleteEventSuccess { get; set; }

        public string DeleteEventMsg { get; set; }
    }
}