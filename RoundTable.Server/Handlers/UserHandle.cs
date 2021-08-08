using System;
using System.Transactions;
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

        public AuthResult Authenticate(LoginUserReqVm model)
        {
            var user = this._userService.GetIdentityVerification(model.Account, model.Password);

            if (user == null)
            {
                return null;
            }

            var expires = DateTime.UtcNow.AddMinutes(3);
            var refreshToken = this._jwtOption.GenerateRefreshToken();
            var jwtToken = this._jwtOption.GenerateJwtToken(user, refreshToken, expires);
            
            // 註冊新 Token
            this._userService.RegisterRefreshTokens(user, refreshToken, expires, true);
            
            return new AuthResult(user, jwtToken, refreshToken, expires);

        }

        public AuthResult RefreshToken(UserInfo user, string refreshToken)
        {
            var regToken = this._userService.GetUserRefreshTokenById(user.Id);

            if (regToken != refreshToken)
            {
                // Refresh Token 異常, 移除無效 Token
                this._userService.RemoveOldRefreshTokens(user); 
                return null;
            }
            
            var expires = DateTime.UtcNow.AddMinutes(15);
            var newRefreshToken = this._jwtOption.GenerateRefreshToken();
            var jwtToken = this._jwtOption.GenerateJwtToken(user, newRefreshToken, expires);
            
            
            //更新 Refresh Token
            this._userService.UpdateRefreshTokens(user, newRefreshToken, expires);
            
            return new AuthResult(user, jwtToken, newRefreshToken, expires);
        }

        public void RemoveAuthenticate(UserInfo user)
        {
            if (user != null)
            {
                this._userService.RemoveOldRefreshTokens(user); 
            }
            
        }
    }
}