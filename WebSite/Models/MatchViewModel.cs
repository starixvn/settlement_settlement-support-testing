using System.Collections.Generic;

namespace WebSite.Models
{
    public class MatchViewModel
    {
        public MatchViewModel()
        {
            ResultInfo = new List<ScoreViewModel>();
        }

        public int MatchId { get; set; }

        public IList<ScoreViewModel> ResultInfo { get; set; }
    }
}