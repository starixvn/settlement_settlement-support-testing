using System.Collections.Generic;
using System.Web.Configuration;
using WebSite.Constants;
using WebSite.Models;

namespace WebSite.Common
{
    public static class Validation
    {
        public static string CurrentSecurityToken
        {
            get
            {
                return WebConfigurationManager.AppSettings["SecurityToken"];
            }
        }

        private static Result ValidScore(IList<Score> resultInfo)
        {
            foreach (var item in resultInfo)
            {
                if (!Helper.IsNumber(item.HomeValue))
                {
                    return new Result { ErrCode = ErrorCode.InvalidHomeValue, ErrMsg = ErrorMessage.InvalidHomeValue };
                }

                if (!Helper.IsNumber(item.AwayValue))
                {
                    return new Result { ErrCode = ErrorCode.InvalidAwayValue, ErrMsg = ErrorMessage.InvalidAwayValue };
                }
            }

            return new Result { ErrMsg = ErrorMessage.Success };
        }

        private static bool InvalidParameter(QueryModel model)
        {
            return model.MatchId < 1 || string.IsNullOrWhiteSpace(model.SecurityToken) || model.ResultInfo == null;
        }

        public static Result ValidResult(QueryModel model)
        {
            if (InvalidParameter(model))
            {
                return new Result { ErrCode = ErrorCode.InvalidParameter, ErrMsg = ErrorMessage.InvalidParameter };
            }

            if (!CurrentSecurityToken.Equals(model.SecurityToken))
            {
                return new Result { ErrCode = ErrorCode.IncorrectSecurityToken, ErrMsg = ErrorMessage.IncorrectSecurityToken };
            }

            return ValidScore(model.ResultInfo);
        }

        public static Result MultiValidation(ListQueryModel model)
        {
            foreach (var item in model.Matches)
            {
                var queryMatch = new QueryModel
                {
                    MatchId = item.MatchId,
                    SecurityToken = model.SecurityToken,
                    ResultInfo = item.ResultInfo
                };

                var result = ValidResult(queryMatch);

                if (result.ErrCode > 0)
                {
                    return result;
                }
            }

            return new Result();
        }
    }
}