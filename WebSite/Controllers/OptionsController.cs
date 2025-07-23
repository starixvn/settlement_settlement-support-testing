using Fanex.RemotingRef;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class OptionsController(IRemotingRefService remotingRefService) : ControllerBase
{
    private const int FourPosition = 4;

    [HttpGet]
    public async Task<IActionResult> GetOptionsByResources(int sportType, string resources)
    {
        // Simulate fetching options from a service or database
        var resourceArray = resources.Split(',').ToList();
        var control = sportType switch
        {
            43 => EsResultControlDictionary.BetTypeToControlGroup,
            6 => VoleyballResultControlDictionary.BetTypeToControlGroup,
            _ => throw new ArgumentException("Invalid sport type")
        };

        var defaultControlGroup = sportType switch
        {
            43 => EsResultControlGroup.HomeAwayNone,
            6 => VoleyballResultControlGroup.CoupleScores,
            _ => throw new ArgumentException("Invalid sport type")
        };

        var controlGroupOptions = sportType switch
        {
            43 => EsResultControlDictionary.ControlGroupToOptions,
            6 => VoleyballResultControlDictionary.VoleyballControlGroupToOptions,
            _ => throw new ArgumentException("Invalid sport type")
        };

        var result = resourceArray.Select((r, index) =>
        {
            var betType = r.Trim()[..FourPosition];
            var controlGroupId = control.GetValueOrDefault(betType, defaultControlGroup);
            var options = controlGroupOptions.GetValueOrDefault(controlGroupId, []);
            return new
            {
                ResourceId = r,
                SportType = sportType,
                BetType = betType,
                betTypeName = remotingRefService.GetBetTypeName(betType, r[FourPosition..]),
                Options = options
            };
        });

        return Ok(result);
    }
}

public record Option(string Value, string Text);

public static class EsResultControl
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

public static class VoleyballResultControl
{
    public static readonly string[] HomeAway = { "3901" };

    public static readonly string[] HomeAwayNone = { "3909" };
    public static readonly string[] YesNo = { "3914" };
    public static readonly string[] CoupleScores = {
        "3903", "3915"
    };
}

public static class EsResultControlGroup
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

public static class VoleyballResultControlGroup
{
    public const int HomeAwayNone = 0;
    public const int HomeAway = 2;
    public const int YesNo = 3;
    public const int CoupleScores = 4;
}

public static class EsResultControlDictionary
{
    // Add this mapping dictionary to convert betType to ResultControlGroup
    public static readonly Dictionary<string, int> BetTypeToControlGroup = new Dictionary<string, int>
    {
        // HomeAway
        { "9067", EsResultControlGroup.HomeAway },
        { "9068", EsResultControlGroup.HomeAway },
        { "9072", EsResultControlGroup.HomeAway },
        
        // HomeAwayDraw
        { "9063", EsResultControlGroup.HomeAwayDraw },
        { "9064", EsResultControlGroup.HomeAwayDraw },
        
        // YesNo
        { "9073", EsResultControlGroup.YesNo },
        { "9075", EsResultControlGroup.YesNo },
        { "9087", EsResultControlGroup.YesNo },
        { "9124", EsResultControlGroup.YesNo },
        { "9132", EsResultControlGroup.YesNo },
        
        // CoupleScores
        { "9002", EsResultControlGroup.CoupleScores },
        { "9008", EsResultControlGroup.CoupleScores },
        { "9012", EsResultControlGroup.CoupleScores },
        { "9018", EsResultControlGroup.CoupleScores },
        { "9024", EsResultControlGroup.CoupleScores },
        { "9028", EsResultControlGroup.CoupleScores },
        { "9034", EsResultControlGroup.CoupleScores },
        { "9040", EsResultControlGroup.CoupleScores },
        { "9046", EsResultControlGroup.CoupleScores },
        { "9052", EsResultControlGroup.CoupleScores },
        { "9059", EsResultControlGroup.CoupleScores },
        { "9074", EsResultControlGroup.CoupleScores },
        { "9065", EsResultControlGroup.CoupleScores },
        { "9066", EsResultControlGroup.CoupleScores },
        { "9069", EsResultControlGroup.CoupleScores },
        { "9091", EsResultControlGroup.CoupleScores },
        { "9093", EsResultControlGroup.CoupleScores },
        { "9115", EsResultControlGroup.CoupleScores },
        { "9139", EsResultControlGroup.CoupleScores },
        { "9140", EsResultControlGroup.CoupleScores },
        
        // Duration
        { "9058", EsResultControlGroup.Duration },
        { "9089", EsResultControlGroup.Duration },
        { "9090", EsResultControlGroup.Duration },
        { "9121", EsResultControlGroup.Duration },
        { "9123", EsResultControlGroup.Duration },
        { "9126", EsResultControlGroup.Duration },
        { "9129", EsResultControlGroup.Duration },
        { "9131", EsResultControlGroup.Duration },
        { "9134", EsResultControlGroup.Duration },
        { "9136", EsResultControlGroup.Duration },
        { "9137", EsResultControlGroup.Duration },
        { "9127", EsResultControlGroup.Duration },
        
        // MidNotMid
        { "9130", EsResultControlGroup.MidNotMid },
        { "9138", EsResultControlGroup.MidNotMid },
        
        // SingleScore
        { "9128", EsResultControlGroup.SingleScore },
        
        // TopBottomNone
        { "9113", EsResultControlGroup.TopBottomNone },
        
        // TopNotTop
        { "9135", EsResultControlGroup.TopNotTop },
        
        // BottomNotBottom
        { "9114", EsResultControlGroup.BottomNotBottom },
        
        // OverlordTyrant
        { "9141", EsResultControlGroup.OverlordTyrant },
        
        // TyrantNotTyrant
        { "9142", EsResultControlGroup.TyrantNotTyrant },
        
        // YesNoNone
        { "9133", EsResultControlGroup.YesNoNone }
    };

    public static readonly Dictionary<int, List<Option>> ControlGroupToOptions = new Dictionary<int, List<Option>>
    {
        { EsResultControlGroup.HomeAwayNone, ResultOptions.HomeAwayNoneOptions},
        { EsResultControlGroup.HomeAway, ResultOptions.HomeAwayOptions },
        { EsResultControlGroup.HomeAwayDraw, ResultOptions.HomeAwayDrawOptions },
        { EsResultControlGroup.YesNo, ResultOptions.YesNoOptions },
        //{ ResultControlGroup.CoupleScores, ResultControl.CoupleScores.ToList() },
        //{ ResultControlGroup.Duration, ResultControl.Duration.ToList() },
        { EsResultControlGroup.MidNotMid, ResultOptions.MidNotMidOptions },
        // { ResultControlGroup.SingleScore, ResultOptions.SingleScoreOptions },
        { EsResultControlGroup.TopBottomNone, ResultOptions.TopBottomNoneOptions },
        { EsResultControlGroup.TopNotTop, ResultOptions.TopNotTopOptions },
        { EsResultControlGroup.BottomNotBottom, ResultOptions.BottomNotBottomOptions },
        { EsResultControlGroup.OverlordTyrant, ResultOptions.OverlordTyrantOptions },
        { EsResultControlGroup.TyrantNotTyrant, ResultOptions.TyrantNotTyrantOptions },
        { EsResultControlGroup.YesNoNone, ResultOptions.YesNoNoneOptions }
    };
}

public static class VoleyballResultControlDictionary
{
    // Add this mapping dictionary to convert betType to ResultControlGroup
    public static readonly Dictionary<string, int> BetTypeToControlGroup = new Dictionary<string, int>
    {
        // HomeAway
        { "3901", VoleyballResultControlGroup.HomeAway },
        
        // YesNo
        { "3914", VoleyballResultControlGroup.YesNo },

        // HomeAwayNone
        { "3909", VoleyballResultControlGroup.HomeAwayNone },

        // CoupleScores
        { "3903", EsResultControlGroup.CoupleScores },
        { "3915", EsResultControlGroup.CoupleScores },
    };

    public static readonly Dictionary<int, List<Option>> ControlGroupToOptions = new Dictionary<int, List<Option>>
    {
        { EsResultControlGroup.HomeAwayNone, ResultOptions.HomeAwayNoneOptions},
        { EsResultControlGroup.HomeAway, ResultOptions.HomeAwayOptions },
        { EsResultControlGroup.HomeAwayDraw, ResultOptions.HomeAwayDrawOptions },
        { EsResultControlGroup.YesNo, ResultOptions.YesNoOptions },
        //{ ResultControlGroup.CoupleScores, ResultControl.CoupleScores.ToList() },
        //{ ResultControlGroup.Duration, ResultControl.Duration.ToList() },
        { EsResultControlGroup.MidNotMid, ResultOptions.MidNotMidOptions },
        // { ResultControlGroup.SingleScore, ResultOptions.SingleScoreOptions },
        { EsResultControlGroup.TopBottomNone, ResultOptions.TopBottomNoneOptions },
        { EsResultControlGroup.TopNotTop, ResultOptions.TopNotTopOptions },
        { EsResultControlGroup.BottomNotBottom, ResultOptions.BottomNotBottomOptions },
        { EsResultControlGroup.OverlordTyrant, ResultOptions.OverlordTyrantOptions },
        { EsResultControlGroup.TyrantNotTyrant, ResultOptions.TyrantNotTyrantOptions },
        { EsResultControlGroup.YesNoNone, ResultOptions.YesNoNoneOptions }
    };


    public static readonly Dictionary<int, List<Option>> VoleyballControlGroupToOptions = new Dictionary<int, List<Option>>
    {
        { VoleyballResultControlGroup.HomeAway, ResultOptions.HomeAwayOptions},
        { VoleyballResultControlGroup.HomeAwayNone, ResultOptions.HomeAwayNoneOptions},
        { VoleyballResultControlGroup.YesNo, ResultOptions.YesNoOptions},
        //{ VoleyballResultControlGroup.CoupleScores, ResultOptions.coy},

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