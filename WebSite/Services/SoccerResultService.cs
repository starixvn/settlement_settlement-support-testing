using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSite.Common;
using WebSite.Constants;
using WebSite.Criteria;
using WebSite.Models;

namespace WebSite.Services
{
    public interface SoccerResultService
    {
        Result Insert(QueryModel model);

        Result InsertSingleMatch(QueryModel model);

        string InsertMultiMatches(IList<Match> matches);
    }

    public class SoccerResultServiceImp : SoccerResultService
    {
        private const int Timer = 45;
        private const string HomeGoal = "H";
        private const string AwayGoal = "A";
        private const string Semicolon = ";";
        private readonly ProviderResultService service;

        public SoccerResultServiceImp(ProviderResultService service)
        {
            this.service = service;
        }

        public int MatchId { get; set; }

        public Result Insert(QueryModel model)
        {
            MatchId = model.MatchId;

            if (model.ResultInfo.Any())
            {
                var results = new List<Result>
                {
                    ProcessForAutoPayout(model.ResultInfo),
                    ProcessForAutoInstant(model.ResultInfo)
                };
                var errorResults = results.Where(result => result.ErrCode > 0);
                return errorResults.Any()
                    ? new Result
                    {
                        ErrCode = ErrorCode.SoccerWayInsertFail,
                        ErrMsg = string.Join("\n", errorResults.Select(result => result.ErrMsg))
                    }
                    : new Result();
            }

            return EmptyScoreInfo();
        }

        private Result ProcessForAutoPayout(List<Score> listScore)
        {
            var fullTimeScore = listScore.GroupBy(s => new { s.GoalTime, s.TotalScore }).Select(g => g.Last()).Where(s => s.TotalScore > 0).ToList();
            var halfTimeScore = fullTimeScore.Where(s => s.GoalTime <= Timer).ToList();
            return fullTimeScore.Any()
                ? AddScoreAndGoal(halfTimeScore, fullTimeScore)
                : EmptyScoreInfo();
        }

        private Result ProcessForAutoInstant(List<Score> listScore)
        {
            var autoInstantScores = listScore.Select(score =>
            {
                return new AutoInstantScore
                {
                    Type = score.GoalTime <= 45 ? (short)GoalType.FirstHalf : (short)GoalType.SecondHalf,
                    HomeValue = string.IsNullOrEmpty(score.HomeValue) ? "0" : score.HomeValue,
                    AwayValue = string.IsNullOrEmpty(score.AwayValue) ? "0" : score.AwayValue
                };
            });
            var autoInstantMatch = new AutoInstantMatch
            {
                Id = MatchId,
                Scores = autoInstantScores.ToArray()
            };
            var resourceXml = Helper.GeneralXmlString(autoInstantMatch);
            var criteria = new InsertMatchScoresCriteria
            {
                SourceId = Sources.SoccerWay,
                ResourceXML = resourceXml
            };
            return service.InsertMatchScores(criteria);
        }

        private Result AddScoreAndGoal(List<Score> halfTimeScore, List<Score> fullTimeScore)
        {
            var error = string.Empty;
            var halfTimeScoreResult = AddScore(halfTimeScore.LastOrDefault(), GoalType.HalfTime);

            if (halfTimeScoreResult.ErrCode > 0)
            {
                error = $"HalfTime Score: {halfTimeScoreResult.ErrMsg}";
            }
            else
            {
                var halfTimeGoalResult = AddGoal(halfTimeScore, GoalType.HalfTimeFirstGoalLastGoal);
                if (halfTimeGoalResult.ErrCode > 0)
                {
                    error = $"HalfTime Goal: {halfTimeGoalResult.ErrMsg}";
                }
            }

            error = halfTimeScore.Count == fullTimeScore.Count ? error : string.Empty;
            var fullTimeScoreResult = AddScore(fullTimeScore.LastOrDefault(), GoalType.FullTime);
            if (fullTimeScoreResult.ErrCode > 0)
            {
                error = string.Join("\n", error, $"Full Time Score: {fullTimeScoreResult.ErrMsg}");
            }
            else
            {
                var fullTimeGoalResult = AddGoal(fullTimeScore, GoalType.FullTimeFirstGoalLastGoal);
                if (fullTimeGoalResult.ErrCode > 0)
                {
                    error = string.Join("\n", error, $"Full Time Goal: {fullTimeGoalResult.ErrMsg}");
                }
            }

            return string.IsNullOrWhiteSpace(error)
                ? new Result()
                : new Result { ErrCode = ErrorCode.SoccerWayInsertFail, ErrMsg = error };
        }

        private Result EmptyScoreInfo()
        {
            var actions = new List<GoalType> { GoalType.HalfTime, GoalType.FullTime, GoalType.HalfTimeFirstGoalLastGoal, GoalType.FullTimeFirstGoalLastGoal };
            var result = new Result();

            foreach (var action in actions)
            {
                var value = action == GoalType.HalfTime || action == GoalType.FullTime ? "0" : string.Empty;
                var criteria = new AddSoccerWayResultCriteria
                {
                    MatchId = MatchId,
                    SourceId = Sources.SoccerWay,
                    TypeId = action,
                    HomeValue = value,
                    AwayValue = value
                };

                if (action == GoalType.FullTimeFirstGoalLastGoal)
                {
                    criteria.HomeValue = Semicolon;
                }

                result = service.InsertSoccerWay(criteria);

                if (result.ErrCode > 0)
                {
                    return result;
                }
            }

            return result;
        }

        private Result AddScore(Score score, GoalType typeId)
        {
            var criteria = new AddSoccerWayResultCriteria
            {
                MatchId = MatchId,
                SourceId = Sources.SoccerWay,
                TypeId = typeId,
                HomeValue = score == null ? "0" : score.HomeValue,
                AwayValue = score == null ? "0" : score.AwayValue
            };

            return service.InsertSoccerWay(criteria);
        }

        private Result AddGoal(List<Score> listScore, GoalType typeId)
        {
            var goal = typeId == GoalType.FullTimeFirstGoalLastGoal ? FullTimeGoalRecord(listScore) : GoalRecord(listScore);
            var criteria = new AddSoccerWayResultCriteria
            {
                MatchId = MatchId,
                SourceId = Sources.SoccerWay,
                TypeId = typeId,
                HomeValue = goal,
                AwayValue = string.Empty
            };

            return service.InsertSoccerWay(criteria);
        }

        private static string GoalRecord(List<Score> listScore)
        {
            var goal = new StringBuilder();

            foreach (var score in listScore)
            {
                goal = score.IsHome ? goal.Append(HomeGoal) : goal.Append(AwayGoal);
            }

            return goal.ToString();
        }

        private static string FullTimeGoalRecord(List<Score> listScore)
        {
            var halfTimeScore = listScore.Where(s => s.GoalTime <= Timer).ToList();
            var fullTimeScore = listScore.Where(s => s.GoalTime > Timer).ToList();

            return string.Join(";", GoalRecord(halfTimeScore), GoalRecord(fullTimeScore));
        }

        public Result InsertSingleMatch(QueryModel model)
        {
            var result = new Result();
            foreach (var score in model.ResultInfo)
            {
                try
                {
                    return service.InsertNormal(model.MatchId, score);
                }
                catch (Exception exception)
                {
                    result.ErrCode = ErrorCode.SystemError;
                    result.ErrMsg = exception.Message;
                }
            }

            return result;
        }

        public string InsertMultiMatches(IList<Match> matches)
        {
            var matchFail = string.Empty;
            foreach (var match in matches)
            {
                var queryModel = new QueryModel { MatchId = match.MatchId, ResultInfo = match.ResultInfo };

                var resultItem = Insert(queryModel);
                if (resultItem.ErrCode > 0)
                {
                    matchFail = string.Join(", ", match.MatchId);
                }
            }

            return matchFail;
        }
    }
}