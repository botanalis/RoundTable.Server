using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Models
{
    public class LoginReult
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public LoginReult(User user, string token)
        {
            UserName = user.UserName;
            Token = token;
        }
    }
}