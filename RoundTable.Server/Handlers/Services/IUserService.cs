using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        LoginReult Authenticate(LoginUser model);

        /// <summary>
        /// 取得使用者,依據 User ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetById(int id);
    }
}