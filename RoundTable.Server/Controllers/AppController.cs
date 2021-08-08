using Microsoft.AspNetCore.Mvc;
using RoundTable.Server.Models;

namespace RoundTable.Server.Controllers
{
   
    public class AppController  : ControllerBase
    {

        public AppController()
        {
        }

        protected UserInfo? GetOptUser()
        {
            return (UserInfo)this.HttpContext?.Items["User"];
        }

        protected string GetOptUserRefreshToken()
        {
            return (string)this.HttpContext?.Items["RefreshToken"];
        }
    }
}