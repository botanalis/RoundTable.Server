using Microsoft.AspNetCore.Mvc;
using RoundTable.Server.Attributes;

namespace RoundTable.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MeetController : AppController
    {

        /// <summary>
        /// 取得會議記錄列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var user = this.GetOptUser();
            return Ok(user);
        }
    }
}