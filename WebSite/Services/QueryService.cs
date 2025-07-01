using AutoMapper;
using Fanex.Data.Repository;
using Fanex.Settlement.Converter.Services;
using WebSite.Criteria;
using WebSite.Models;

namespace WebSite.Services;

public interface QueryService
{
    Task<CloneResultDetailModel> Get(int matchId);

    Task<Result> PushScore(Models.ExternalResults.QueryModel request);

    int GetMatchBySportType(SportTypeRequest request);

    IEnumerable<DeleteEventResponse> DeleteMatch(DeleteMatchRequest request);
}

public class QueryServiceImp(IDynamicRepository repository, XmlConvertService xmlConvertService) : QueryService
{
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

    public async Task<Result> PushScore(Models.ExternalResults.QueryModel request)
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

    public int GetMatchBySportType(SportTypeRequest request)
    {
        var criteria = new GetMatchBySportCriteria
        {
            SportType = request.Id,
            IsTicket = request.IsTicket
        };

        var match = repository.Get<Match>(criteria);
        return match.MatchId;
    }

    public IEnumerable<DeleteEventResponse> DeleteMatch(DeleteMatchRequest request)
    {
        var result = new List<DeleteEventResponse>();
        foreach (var requestMatchId in request.MatchIds)
        {
            if (requestMatchId % 2 == 0)
            {
                var resultByMatchId = new DeleteEventResponse(requestMatchId, true, "MatchSuccess");
                result.Add(resultByMatchId);
            }
            else
            {
                var resultByMatchId = new DeleteEventResponse(requestMatchId, false, "MatchFail");
                result.Add(resultByMatchId);
            }
        }
        return result;
    }
}