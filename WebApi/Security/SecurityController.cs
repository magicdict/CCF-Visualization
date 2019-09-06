using Microsoft.AspNetCore.Mvc;

public class SecurityController : ControllerBase
{

    public static DashBoard _dashboard = null;

    [HttpGet("GetDashBoard")]
    public ActionResult<DashBoard> GetDashBoard()
    {
        if (_dashboard == null)
        {
            _dashboard = new DashBoard();
        }
        return _dashboard;
    }


    public static TimeAnalysis _timeanalysis = null;
    [HttpGet("GetTimeAnalysis")]
    public ActionResult<TimeAnalysis> GetTimeAnalysis()
    {
        if (_timeanalysis == null)
        {
            _timeanalysis = new TimeAnalysis();
        }
        return _timeanalysis;
    }
}