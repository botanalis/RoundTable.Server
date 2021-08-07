using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Interfaces.Utils
{
    public interface IJwtOption
    {
        /// <summary>
        /// 產生 JWT Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateJwtToken(User user);
        
        
        /// <summary>
        /// 驗證 JWT Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int? ValidateJwtToken(string token);
        
    }
}