using RoundTable.Server.Models;

namespace RoundTable.Server.Interfaces.Handlers
{
    public interface IUserHandle
    {
        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AuthResult Authenticate(LoginUserReqVm model);

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        AuthResult RefreshToken(UserInfo user, string refreshToken);

        /// <summary>
        /// 移除授權
        /// </summary>
        /// <param name="user"></param>
        void RemoveAuthenticate(UserInfo user);
    }
}