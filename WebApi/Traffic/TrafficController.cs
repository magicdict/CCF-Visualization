using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class TrafficController : ControllerBase
{
    public static TrafficTimeAnalysis _timeanalysis = null;
    [HttpGet("GetTimeAnalysis")]
    public ActionResult<TrafficTimeAnalysis> TrafficTimeAnalysis()
    {
        if (_timeanalysis == null)
        {
            _timeanalysis = new TrafficTimeAnalysis();
        }
        return _timeanalysis;
    }
}