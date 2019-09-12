using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class TrafficController : ControllerBase
{

    public static TrafficDashBoard _dashboard = null;

    [HttpGet("GetDashBoard")]
    public ActionResult<TrafficDashBoard> GetDashBoard()
    {
        if (_dashboard == null)
        {
            _dashboard = new TrafficDashBoard();
        }
        return _dashboard;
    }

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


    [HttpGet("GetDiaryinfos")]
    public ActionResult<List<NameValueSet<DiaryInfo>>> GetDiaryinfos()
    {
        return TrafficDataSet.diaryinfos;
    }

}