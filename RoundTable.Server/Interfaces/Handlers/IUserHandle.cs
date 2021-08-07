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
        LoginReult Authenticate(LoginUser model);
    }
}