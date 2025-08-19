using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

public class Market
{
    public int MatchId { get; set; }
    public int BetType { get; set; }

    public decimal GoalLine { get; set; }

    public int LiveHomeScore { get; set; }

    public int LiveAwayScore { get; set; }
}
public static class RequestMonitorService
{
    private static readonly ConcurrentQueue<string> _requests = new ConcurrentQueue<string>();

    private static List<Market> _markets = new List<Market>();

    public static void AddMarket(string markets)
    {
        var listMarket = markets.Replace("(", string.Empty).Replace(")", string.Empty).Split(',');
        foreach (var market in listMarket)
        {
            var trimmedMarket = market.Trim();
            var dashIndex = trimmedMarket.IndexOf('-');
            if (dashIndex > 0)
            {
                var betTypeStr = trimmedMarket.Substring(0, dashIndex).Trim();
                var goalLineStr = trimmedMarket.Substring(dashIndex + 1).Trim();

                if (int.TryParse(betTypeStr, out int betType) && decimal.TryParse(goalLineStr, out decimal goalLine))
                {
                    _markets.Add(new Market
                    {
                        BetType = betType,
                        GoalLine = goalLine,
                        LiveHomeScore = 0,
                        LiveAwayScore = 0
                    });
                }
            }
        }
    }

    public static void UpdateLiveScore(int betType, decimal goalLine, int homeScore, int awayScore)
    {
        var market = _markets.FirstOrDefault(m => m.BetType == betType && m.GoalLine == goalLine);
        if (market != null)
        {
            market.LiveHomeScore = homeScore;
            market.LiveAwayScore = awayScore;
        }
    }

    public static void AddRequest(string requestData, string apiName)
    {
        _requests.Enqueue($"{(DateTime.Now.ToString("HH:mm:ss"))}: API {apiName} - {requestData}");

        while (_requests.Count > 50)
        {
            _requests.TryDequeue(out _);
        }
    }

    public static IEnumerable<string> GetRequests()
    {
        return _requests.Reverse();
    }

    public static List<Market> GetMarket()
    {
        return _markets;
    }

    public static void ClearMarket(Market market)
    {
        _markets.RemoveAll(m => m.BetType == market.BetType && m.GoalLine == market.GoalLine);
    }
}