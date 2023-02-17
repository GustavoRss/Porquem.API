using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Email;
using PQ.CoreShared.ModelViews.User;
using PQ.CoreShared.ModelViews.Usuario;
using PQ.Manager.Interfaces;
using PQ.Manager.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly IEmailService email;

        public UserController(IUserManager manager, IEmailService email)
        {
            this.manager = manager;
            this.email = email;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var loggedUser = await manager.ValidateUserAndGenerateToken(user);
            if (loggedUser != null)
            {
                return Ok(loggedUser);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string login = User.Identity.Name;
            var usuario = await manager.GetAsync(login);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewUser user)
        {
            var includedUser = await manager.InsertAsync(user);
            return CreatedAtAction(nameof(Get), new { login = user.Email }, includedUser);
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            
            bool confirm = manager.ResetPassword(model);
            if (confirm)
            {
                return Ok(new { message = "Sua senha foi alterada com sucesso!" });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifyToken(ResetPasswordModel model)
        {

            bool confirm = manager.verifyToken(model);
            if (confirm)
            {
                return Ok(confirm);
            }
            else
            {
                return NotFound(new { message = "Não encontrado" });
            }
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(EmailModel model)
        {
            bool confirm = manager.ForgotPassword(model, Request.Headers["origin"]);
            if (confirm)
            {
                return Ok(new { message = "Verifique seu email para seguir as instruções de recuperação de senha" });
            }
            else
            {
                return NotFound(new { message = "Esse e-mail não está cadastrado" });
            }
        }

    }
}
