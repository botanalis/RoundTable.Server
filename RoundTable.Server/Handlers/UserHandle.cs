using RoundTable.Server.Interfaces.Handlers;
using RoundTable.Server.Interfaces.Services;
using RoundTable.Server.Interfaces.Utils;
using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers
{
    public class UserHandle : IUserHandle
    {
        private readonly IUserService _userService;
        
        private readonly IJwtOption _jwtOption;

        public UserHandle(
            IJwtOption jwtOption,
            IUserService userService)
        {
            this._userService = userService;
            this._jwtOption = jwtOption;
        }

        public LoginReult Authenticate(LoginUser model)
        {
            var user = this._userService.GetIdentityVerification(model.Account, model.Password);

            if (user == null)
            {
                return null;
            }
            
            var token = this._jwtOption.GenerateJwtToken(user);

            return new LoginReult(user, token);
        }
    }
}