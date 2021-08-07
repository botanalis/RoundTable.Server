using RoundTable.Server.Data.Model;
using RoundTable.Server.Models;

namespace RoundTable.Server.Interfaces.Services
{
    public interface IUserService
    {
        
        /// <summary>
        /// 取得使用者,依據 User ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetById(int id);

        /// <summary>
        /// 取得身分
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        User GetIdentityVerification(string account, string password);
    }
}