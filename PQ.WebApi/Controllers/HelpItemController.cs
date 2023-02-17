using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Campaign;
using PQ.CoreShared.ModelViews.HelpItem;
using PQ.Manager.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpItemController : ControllerBase
    {
        private readonly IHelpItemManager helpItemManager;

        public HelpItemController(IHelpItemManager helpItemManager)
        {
            this.helpItemManager = helpItemManager;
        }


        [ProducesResponseType(typeof(ViewHelpItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await helpItemManager.GetHelpItemsAsync());
        }

        [ProducesResponseType(typeof(ViewHelpItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await helpItemManager.GetHelpItemAsync(id);
            if (request == null)
            {
                return NotFound("Nenhuma categoria encontrada com esse código");
            }
            else
            {
                return Ok(request);
            }

        }
        [ProducesResponseType(typeof(ViewHelpItem), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await helpItemManager.DeleteHelpItemAsync(id);
            return NoContent();
        }
        [ProducesResponseType(typeof(NewHelpItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpPost]
        public async Task<IActionResult> Post(NewHelpItem helpItem)
        {

            await helpItemManager.InsertHelpItemAsync(helpItem);
            
            return Ok(helpItem);

        }
    }
}
