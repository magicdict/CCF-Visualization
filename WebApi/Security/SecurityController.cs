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
}