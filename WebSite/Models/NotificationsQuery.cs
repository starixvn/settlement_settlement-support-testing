namespace WebSite.Models;

public class NotificationServiceIndex
{
    public int Index { get; set; } = 1;
}

public class BetId
{
    public string Id { get; set; } = string.Empty;
}

public class MatchId
{
    public string Id { get; set; } = string.Empty;
}

public class Bet
{
    public BetId BetId { get; set; } = new BetId();
}

public class BetBuilderOdds
{
    public Bet Bet { get; set; } = new Bet();
    public MatchId MatchId { get; set; } = new MatchId();
    public double Odds { get; set; }
}

public class BetBuilderNotification
{
    public BetBuilderOdds BetBuilderOdds { get; set; } = new BetBuilderOdds();
}

public class BetBuilderParentNotifications
{
    public string Offset { get; set; } = string.Empty;
    public List<BetBuilderNotification> Notifications { get; set; } = new List<BetBuilderNotification>();
}

public class BetBuilderQueryResult
{
    public string ResultCode { get; set; } = "SUCCESS";
    public BetBuilderParentNotifications Notifications { get; set; } = new BetBuilderParentNotifications();
}

public class BetBuilderQuery
{
    public NotificationServiceIndex Service { get; set; } = new NotificationServiceIndex { Index = 0 };
    public BetBuilderQueryResult QueryResult { get; set; } = new BetBuilderQueryResult();
}