using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Models
{
    public class LoginResultResVm
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public LoginResultResVm(UserInfo user, string token)
        {
            UserName = user.UserName;
            Token = token;
        }
    }
}