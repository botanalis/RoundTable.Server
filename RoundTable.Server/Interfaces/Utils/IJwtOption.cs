using System;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Models;

namespace RoundTable.Server.Interfaces.Utils
{
    public interface IJwtOption
    {
        /// <summary>
        /// 產生 JWT Token
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <param name="expires">有效期限</param>
        /// <returns></returns>
        string GenerateJwtToken(UserInfo user, string refreshToken, DateTime? expires);
        
        
        /// <summary>
        /// 驗證 JWT Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        JwtTokenInfo ValidateJwtToken(string token);

        /// <summary>
        /// 產生 Refresh Token
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();
    }
}