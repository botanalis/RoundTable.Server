using Microsoft.AspNetCore.Mvc;
using RoundTable.Server.Attributes;
using RoundTable.Server.Handlers.Services;
using RoundTable.Server.Interfaces.Handlers;
using RoundTable.Server.Interfaces.Services;
using RoundTable.Server.Models;

namespace RoundTable.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : AppController
    {
        private readonly IUserHandle _userHandle;

        public UsersController(IUserHandle userHandle)
        {
            _userHandle = userHandle;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginUser model)
        {
            var result = this._userHandle.Authenticate(model);

            if (result == null)
            {
                return BadRequest(new {message = "Account or Password is Error"});
            }

            return Ok(result);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RefreshToken()
        {
            var user = this.GetOptUser();
            return Ok();
        }

    }
}