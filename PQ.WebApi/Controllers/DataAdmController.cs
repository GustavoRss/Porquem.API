using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using PQ.Manager.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAdmController : ControllerBase
    {
        private readonly IDataAdmManager dataAdmManager;

        public DataAdmController(IDataAdmManager dataAdmManager)
        {
            this.dataAdmManager = dataAdmManager;
        }

        
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dataAdmManager.GetEntitiesAsync());
        }

        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await dataAdmManager.GetEntityAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(request);
            }

        }

        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ChangeStatus alteraStatus, int id)
        {
            alteraStatus.Id = id;
            var entidadeAtualizada = await dataAdmManager.UpdateEntityAsync(alteraStatus);
            if (entidadeAtualizada == null)
            {
                return NotFound();
            }
            return Ok(alteraStatus);

        }
    }
}
