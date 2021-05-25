using AutoMapper;
using Fanex.Data;
using Fanex.Data.Repository;
using Kaden.XmlConverter;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Criteria;
using WebSite.Models;
using WebSite.Models.ExternalResults;

namespace WebSite.Services
{
    public class QueryService
    {
        private readonly IDynamicRepository repository;

        private readonly XmlConvertService xmlConvertService;

        public QueryService()
        {
            repository = new DynamicRepository();
            xmlConvertService = new XmlConvertServiceImp();
        }

        public async Task<CloneResultDetailModel> Get(int matchId)
        {
            var criteria = new GetResultDetailCriteria
            {
                MatchId = matchId
            };

            var data = await repository.FetchMultipleAsync<ResultDetailModel, ExternalResultDetailModel>(criteria);

            return new CloneResultDetailModel
            {
                ResultDetails = data.Item1.ToList(),
                ExternalResultDetails = data.Item2.ToList()
            };
        }

        public async Task<Result> Clone(CloneResultDetailModel request)
        {
            var resultDetail = ResultDetail(request.ResultDetails);
            var externalResultDetail = ExternalResultDetail(request.ExternalResultDetails);

            var criteria = new CloneResultDetailCriteria
            {
                MatchId = request.MatchId,
                ResultDetail = resultDetail,
                ExternalResult = externalResultDetail
            };

            await repository.ExecuteAsync(criteria);

            return new Result
            {
                ErrCode = criteria.ErrCode,
                ErrMsg = criteria.ErrMsg
            };
        }

        private string ResultDetail(List<ResultDetailModel> resultDetails)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResultDetailModel, ResourcesItem>();
            });
            var iMapper = config.CreateMapper();

            var resourceItems = new List<ResourcesItem>();
            foreach (var resultDetail in resultDetails)
            {
                var item = iMapper.Map<ResultDetailModel, ResourcesItem>(resultDetail);
                resourceItems.Add(item);
            }

            var resources = new Models.Resources
            {
                ResourceItems = resourceItems
            };

            return xmlConvertService.Serialize(resources);
        }

        private string ExternalResultDetail(List<ExternalResultDetailModel> externalResultDetails)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ExternalResultDetailModel, ExternalResourcesItem>();
            });
            var iMapper = config.CreateMapper();

            var resourceItems = new List<ExternalResourcesItem>();
            foreach (var resultDetail in externalResultDetails)
            {
                var item = iMapper.Map<ExternalResultDetailModel, ExternalResourcesItem>(resultDetail);

                resourceItems.Add(item);
            }

            var resources = new ExternalResources
            {
                ResourceItems = resourceItems
            };

            return xmlConvertService.Serialize(resources);
        }

        public async Task<DataSet> GetBalance(GetBalanceRequest request)
        {
            var criteria = new GetBalanceCriteria
            {
                MatchId = request.MatchId,
                CustId = request.CustId
            };

            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                return await objectDb.ExecuteDataSetAsync(criteria).ConfigureAwait(false);
            }
        }

        public async Task<Result> PushScore(QueryModel request)
        {
            var criteria = new PushExternalResultCriteria
            {
                MatchId3rd = request.MatchId3rd,
                MatchId = request.MatchId,
                SourceId = request.SourceId,
            };

            var resources = new Models.ExternalResults.Resources
            {
                ResourceItems = request.Results
            };

            criteria.Resources = xmlConvertService.Serialize(resources);
            await repository.ExecuteAsync(criteria);

            return new Result
            {
                ErrCode = criteria.ErrCode,
                ErrMsg = criteria.ErrMsg
            };
        }
    }
}