using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Campaign;
using PQ.Manager.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorManager visitorManager;

        public VisitorController(IVisitorManager visitorManager)
        {
            this.visitorManager = visitorManager;
        }


        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("Campaign")]
        public async Task<IActionResult> Get()
        {

            try
            {
                var request = await visitorManager.GetCampaignsAsync();
                if (request == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(request);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    ex);
            }
        }

        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("Campaign/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var request = await visitorManager.GetCampaignAsync(id);
                if (request == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(request);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar a campanha.");
            }

        }
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("PhilanthropicEntity/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await visitorManager.GetEntityAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(request);
            }

        }
    }
}
