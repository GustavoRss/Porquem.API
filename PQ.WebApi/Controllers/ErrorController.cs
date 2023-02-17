using Microsoft.AspNetCore.Mvc;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {

            Response.StatusCode = 500;
            var id = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            return new ErrorResponse(id);
        }
    }
}
