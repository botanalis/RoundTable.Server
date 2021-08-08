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
        public IActionResult Login(LoginUserReqVm model)
        {
            var result = this._userHandle.Authenticate(model);

            if (result == null)
            {
                return BadRequest(new {message = "Account or Password is Error"});
            }
            
            return Ok(new LoginResultResVm(result.UserInfo, result.JwtToken));
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogOut()
        {
            var user = this.GetOptUser();

            this._userHandle.RemoveAuthenticate(user);
            

            return Ok();
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RefreshToken()
        {
            var user = this.GetOptUser();
            var refreshToken = this.GetOptUserRefreshToken();

            var result = this._userHandle.RefreshToken(user, refreshToken);

            if (result == null)
            {
                Response.Cookies.Delete("refreshToken");
                return BadRequest(new { message = "Refresh Token is invalid" });
            }
            
            
            return Ok(new LoginResultResVm(result.UserInfo, result.JwtToken));
        }

    }
}