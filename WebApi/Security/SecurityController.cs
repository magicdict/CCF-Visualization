using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class SecurityController : ControllerBase
{
    public static SecurityDashBoard _dashboard = null;

    [HttpGet("GetDashBoard")]
    public ActionResult<SecurityDashBoard> GetDashBoard()
    {
        if (_dashboard == null)
        {
            _dashboard = new SecurityDashBoard();
        }
        return _dashboard;
    }


    public static SecurityTimeAnalysis _timeanalysis = null;
    [HttpGet("GetTimeAnalysis")]
    public ActionResult<SecurityTimeAnalysis> GetTimeAnalysis()
    {
        if (_timeanalysis == null)
        {
            _timeanalysis = new SecurityTimeAnalysis();
        }
        return _timeanalysis;
    }
}