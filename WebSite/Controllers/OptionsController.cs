using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class OptionsController : ControllerBase
{
    private const int FourPosition = 4;

    [HttpGet]
    public async Task<IActionResult> GetOptionsByResources(int sportType, string resources)
    {
        // Simulate fetching options from a service or database
        var resourceArray = resources.Split(',').ToList();

        var result = resourceArray.Select((r, index) =>
        {
            var betType = r.Trim()[..FourPosition];
            var controlGroupId = ResultControlDictionary.BetTypeToControlGroup.GetValueOrDefault(betType, ResultControlGroup.HomeAwayNone);
            var options = ResultControlDictionary.ControlGroupToOptions.GetValueOrDefault(controlGroupId, []);
            return new
            {
                ResourceId = r,
                BetType = betType,
                Options = options
            };
        });

        return Ok(result);
    }
}

public record Option(string Value, string Text);

public static class ResultControl
{
    public static readonly string[] HomeAway = { "9067", "9068", "9072" };
    public static readonly string[] HomeAwayDraw = { "9063", "9064" };
    public static readonly string[] YesNo = { "9073", "9075", "9087", "9124", "9132" };
    public static readonly string[] CoupleScores = {
        "9002", "9008", "9012", "9018", "9024", "9028", "9034", "9040", "9046", "9052",
        "9059", "9074", "9065", "9066", "9069", "9091", "9093",
        "9115", "9139", "9140"
    };
    public static readonly string[] Duration = {
        "9058", "9089", "9090", "9121", "9123", "9126", "9129", "9131", "9134", "9136", "9137", "9127"
    };
    public static readonly string[] MidNotMid = { "9130", "9138" };
    public static readonly string[] MoneyLine = { "9115" };
    public static readonly string[] SingleScore = { "9128" };
    public static readonly string[] TopBottomNone = { "9113" };
    public static readonly string[] TopNotTop = { "9135" };
    public static readonly string[] BottomNotBottom = { "9114" };
    public static readonly string[] OverlordTyrant = { "9141" };
    public static readonly string[] TyrantNotTyrant = { "9142" };
    public static readonly string[] YesNoNone = { "9133" };
}

public static class ResultControlGroup
{
    public const int HomeAwayNone = 0;
    public const int HomeAway = 1;
    public const int HomeAwayDraw = 2;
    public const int YesNo = 3;
    public const int CoupleScores = 4;
    public const int Duration = 5;
    public const int MidNotMid = 6;
    public const int SingleScore = 7;
    public const int TopBottomNone = 8;
    public const int TopNotTop = 9;
    public const int BottomNotBottom = 10;
    public const int OverlordTyrant = 11;
    public const int TyrantNotTyrant = 12;
    public const int YesNoNone = 13;
}

public static class ResultControlDictionary
{
    // Add this mapping dictionary to convert betType to ResultControlGroup
    public static readonly Dictionary<string, int> BetTypeToControlGroup = new Dictionary<string, int>
    {
        // HomeAway
        { "9067", ResultControlGroup.HomeAway },
        { "9068", ResultControlGroup.HomeAway },
        { "9072", ResultControlGroup.HomeAway },
        
        // HomeAwayDraw
        { "9063", ResultControlGroup.HomeAwayDraw },
        { "9064", ResultControlGroup.HomeAwayDraw },
        
        // YesNo
        { "9073", ResultControlGroup.YesNo },
        { "9075", ResultControlGroup.YesNo },
        { "9087", ResultControlGroup.YesNo },
        { "9124", ResultControlGroup.YesNo },
        { "9132", ResultControlGroup.YesNo },
        
        // CoupleScores
        { "9002", ResultControlGroup.CoupleScores },
        { "9008", ResultControlGroup.CoupleScores },
        { "9012", ResultControlGroup.CoupleScores },
        { "9018", ResultControlGroup.CoupleScores },
        { "9024", ResultControlGroup.CoupleScores },
        { "9028", ResultControlGroup.CoupleScores },
        { "9034", ResultControlGroup.CoupleScores },
        { "9040", ResultControlGroup.CoupleScores },
        { "9046", ResultControlGroup.CoupleScores },
        { "9052", ResultControlGroup.CoupleScores },
        { "9059", ResultControlGroup.CoupleScores },
        { "9074", ResultControlGroup.CoupleScores },
        { "9065", ResultControlGroup.CoupleScores },
        { "9066", ResultControlGroup.CoupleScores },
        { "9069", ResultControlGroup.CoupleScores },
        { "9091", ResultControlGroup.CoupleScores },
        { "9093", ResultControlGroup.CoupleScores },
        { "9115", ResultControlGroup.CoupleScores },
        { "9139", ResultControlGroup.CoupleScores },
        { "9140", ResultControlGroup.CoupleScores },
        
        // Duration
        { "9058", ResultControlGroup.Duration },
        { "9089", ResultControlGroup.Duration },
        { "9090", ResultControlGroup.Duration },
        { "9121", ResultControlGroup.Duration },
        { "9123", ResultControlGroup.Duration },
        { "9126", ResultControlGroup.Duration },
        { "9129", ResultControlGroup.Duration },
        { "9131", ResultControlGroup.Duration },
        { "9134", ResultControlGroup.Duration },
        { "9136", ResultControlGroup.Duration },
        { "9137", ResultControlGroup.Duration },
        { "9127", ResultControlGroup.Duration },
        
        // MidNotMid
        { "9130", ResultControlGroup.MidNotMid },
        { "9138", ResultControlGroup.MidNotMid },
        
        // SingleScore
        { "9128", ResultControlGroup.SingleScore },
        
        // TopBottomNone
        { "9113", ResultControlGroup.TopBottomNone },
        
        // TopNotTop
        { "9135", ResultControlGroup.TopNotTop },
        
        // BottomNotBottom
        { "9114", ResultControlGroup.BottomNotBottom },
        
        // OverlordTyrant
        { "9141", ResultControlGroup.OverlordTyrant },
        
        // TyrantNotTyrant
        { "9142", ResultControlGroup.TyrantNotTyrant },
        
        // YesNoNone
        { "9133", ResultControlGroup.YesNoNone }
    };

    public static readonly Dictionary<int, List<Option>> ControlGroupToOptions = new Dictionary<int, List<Option>>
    {
         { ResultControlGroup.HomeAwayNone, ResultOptions.HomeAwayNoneOptions},
        { ResultControlGroup.HomeAway, ResultOptions.HomeAwayOptions },
        { ResultControlGroup.HomeAwayDraw, ResultOptions.HomeAwayDrawOptions },
        { ResultControlGroup.YesNo, ResultOptions.YesNoOptions },
        //{ ResultControlGroup.CoupleScores, ResultControl.CoupleScores.ToList() },
        //{ ResultControlGroup.Duration, ResultControl.Duration.ToList() },
        { ResultControlGroup.MidNotMid, ResultOptions.MidNotMidOptions },
        // { ResultControlGroup.SingleScore, ResultOptions.SingleScoreOptions },
        { ResultControlGroup.TopBottomNone, ResultOptions.TopBottomNoneOptions },
        { ResultControlGroup.TopNotTop, ResultOptions.TopNotTopOptions },
        { ResultControlGroup.BottomNotBottom, ResultOptions.BottomNotBottomOptions },
        { ResultControlGroup.OverlordTyrant, ResultOptions.OverlordTyrantOptions },
        { ResultControlGroup.TyrantNotTyrant, ResultOptions.TyrantNotTyrantOptions },
        { ResultControlGroup.YesNoNone, ResultOptions.YesNoNoneOptions }
    };
}


// Tạo list các option
public static class ResultOptions
{
    // Case 0: HomeAwayNone
    public static readonly List<Option> HomeAwayNoneOptions = new List<Option>
    {
        new Option("H", "Home"),
        new Option("A", "Away"),
        new Option("N", "None")
    };

    // Case 1: HomeAway
    public static readonly List<Option> HomeAwayOptions = new List<Option>
    {
        new Option("H", "Home"),
        new Option("A", "Away")
    };

    // Case 2: HomeAwayDraw
    public static readonly List<Option> HomeAwayDrawOptions = new List<Option>
    {
        new Option("H", "Home"),
        new Option("A", "Away"),
        new Option("D", "Draw")
    };

    // Case 3: YesNo
    public static readonly List<Option> YesNoOptions = new List<Option>
    {
        new Option("Y", "Yes"),
        new Option("N", "No")
    };

    // Case 6: MidNotMid
    public static readonly List<Option> MidNotMidOptions = new List<Option>
    {
        new Option("M", "Middle"),
        new Option("NM", "Top or Bottom"),
        new Option("N", "None")
    };

    // Case 8: TopBottomNone
    public static readonly List<Option> TopBottomNoneOptions = new List<Option>
    {
        new Option("T", "Top"),
        new Option("B", "Bottom"),
        new Option("N", "None")
    };

    // Case 9: TopNotTop
    public static readonly List<Option> TopNotTopOptions = new List<Option>
    {
        new Option("T", "Top"),
        new Option("NT", "Middle or Bottom"),
        new Option("N", "None")
    };

    // Case 10: BottomNotBottom
    public static readonly List<Option> BottomNotBottomOptions = new List<Option>
    {
        new Option("B", "Bottom"),
        new Option("NB", "Top or Middle"),
        new Option("N", "None")
    };

    // Case 11: OverlordTyrant
    public static readonly List<Option> OverlordTyrantOptions = new List<Option>
    {
        new Option("O", "Overlord"),
        new Option("T", "Tyrant"),
        new Option("N", "None")
    };

    // Case 12: TyrantNotTyrant
    public static readonly List<Option> TyrantNotTyrantOptions = new List<Option>
    {
        new Option("T", "Tyrant"),
        new Option("NT", "Overlord or Middle"),
        new Option("N", "None")
    };

    // Case 13: YesNoNone
    public static readonly List<Option> YesNoNoneOptions = new List<Option>
    {
        new Option("Y", "Yes"),
        new Option("N", "No"),
        new Option("NONE", "None")
    };
}