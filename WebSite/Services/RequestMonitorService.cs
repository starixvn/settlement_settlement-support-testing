using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

public class Market
{
    public int MatchId { get; set; }
    public int BetType { get; set; }

    public decimal GoalLine { get; set; }
}
public static class RequestMonitorService
{
    private static readonly ConcurrentQueue<string> _requests = new ConcurrentQueue<string>();

    private static List<Market> _markets = new List<Market>();

    public static void AddMarket(string markets)
    {
        _markets = new List<Market>();
        var listMarket = markets.Split(',');
        foreach (var market in listMarket)
        {
            var marketData = market.Split('-');
            _markets.Add(new Market
            {
                BetType = int.Parse(marketData[0]),
                GoalLine = decimal.Parse(marketData[1])
            });
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
}