using WebSite.Models;

namespace WebSite.Services
{
    public class BetBuilderQueryService
    {
        private readonly Random _random = new Random();
        public BetBuilderQuery CreateSampleQuery()
        {
            return new BetBuilderQuery
            {
                Service = new NotificationServiceIndex { Index = 0 },
                QueryResult = new BetBuilderQueryResult
                {
                    ResultCode = "SUCCESS",
                    Notifications = new BetBuilderParentNotifications
                    {
                        Offset = "10",
                        Notifications = new List<BetBuilderNotification>
                        {
                            new BetBuilderNotification
                            {
                                BetBuilderOdds = new BetBuilderOdds
                                {
                                    Bet = new Bet
                                    {
                                        BetId = new BetId { Id = "448310400052625408" }
                                    },
                                    MatchId = new MatchId { Id = "83820770" },
                                    Odds = GetRandomOdds()
                                }
                            },
                            new BetBuilderNotification
                            {
                                BetBuilderOdds = new BetBuilderOdds
                                {
                                    Bet = new Bet
                                    {
                                        BetId = new BetId { Id = "448310400052625408" }
                                    },
                                    MatchId = new MatchId { Id = "83820771" },
                                    Odds = GetRandomOdds()
                                }
                            },
                            new BetBuilderNotification
                            {
                                BetBuilderOdds = new BetBuilderOdds
                                {
                                    Bet = new Bet
                                    {
                                        BetId = new BetId { Id = "448310400052625408" }
                                    },
                                    MatchId = new MatchId { Id = "83820772" },
                                    Odds = GetRandomOdds()
                                }
                            }
                        }
                    }
                }
            };
        }

        private double GetRandomOdds()
        {
            // Tạo odds ngẫu nhiên từ 1.01 đến 10.00 với 2 chữ số thập phân
            return Math.Round(_random.NextDouble() * 8.99 + 1.01, 2);
        }
    }
}
