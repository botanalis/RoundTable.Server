using Microsoft.AspNetCore.Mvc;
using RoundTable.Server.Handlers.Services;
using RoundTable.Server.Models;

namespace RoundTable.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginUser model)
        {
            var result = this._userService.Authenticate(model);

            if (result == null)
            {
                return BadRequest(new {message = "Account or Password is Error"});
            }

            return Ok(result);
        }
    }
}