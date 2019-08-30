using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class SecurityController : ControllerBase
{



    [HttpGet("GetProtocols")]
    public ActionResult<List<NameValueSet<int>>> GetProtocols()
    {
       return DataSet.Protocols;
    }
}