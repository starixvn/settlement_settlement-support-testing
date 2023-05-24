using Fanex.Data.Repository;
using WebSite.Criteria;
using WebSite.Models;

namespace WebSite.Services
{
    public interface ProviderResultService
    {
        Result InsertSoccerWay(AddSoccerWayResultCriteria criteria);

        Result InsertMatchScores(InsertMatchScoresCriteria criteria);

        Result InsertNormal(int matchId, Score score);
    }

    public class ProviderResultServiceImp : ProviderResultService
    {
        private readonly IDynamicRepository repository;

        public ProviderResultServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public Result InsertSoccerWay(AddSoccerWayResultCriteria criteria)
        {
            repository.Execute(criteria);

            return new Result
            {
                ErrCode = criteria.ErrCode,
                ErrMsg = criteria.ErrMsg
            };
        }

        public Result InsertMatchScores(InsertMatchScoresCriteria criteria)
        {
            repository.Execute(criteria);

            return new Result
            {
                ErrCode = criteria.ErrCode,
                ErrMsg = criteria.ErrMsg
            };
        }

        public Result InsertNormal(int matchId, Score score)
        {
            var criteria = new AddResultCriteria
            {
                MatchId = matchId,
                SourceId = score.SourceId,
                TypeId = score.TypeId,
                HomeValue = score.HomeValue,
                AwayValue = score.AwayValue
            };

            repository.Execute(criteria);

            return new Result
            {
                ErrCode = criteria.ErrCode,
                ErrMsg = criteria.ErrMsg
            };
        }
    }
}